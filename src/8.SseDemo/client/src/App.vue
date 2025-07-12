<script setup>
import { onMounted, onUnmounted } from 'vue';
import HelloWorld from './components/HelloWorld.vue'

let eventSource = null;
const userId = 1;
const token = '111222';

const initSSEConnection = () => {
  if (eventSource) {
    eventSource.close();
  }
  eventSource = new EventSource(`http://localhost:5175/api/SseService/TestClientStream/${token}?userId=${userId}`);
  //eventSource = new EventSource(`http://localhost:5175/api/SseService/TestServerStream/${token}?userId=${userId}`);
  // eventSource.onmessage = (event) => {
  //   console.log(event.data);
  //   //console.log(eventSource);
  // }
  // eventSource.onerror = (event) => {
  //   console.error('EventSource failed:', event.data);
  // }
  // eventSource.onopen = () => {
  //   console.log('EventSource opened');
  // }
  eventSource.addEventListener('message', (event) => {
    console.log("message:",event.data);
  });
  eventSource.addEventListener('error', (event) => {
    console.error('EventSource failed');
  });
  eventSource.addEventListener('open', () => {  
    console.log('EventSource opened');
  });

  eventSource.addEventListener('heatbeat', () => {
    console.log("heatbeat");
  });

  eventSource.addEventListener('chat', (event) => {
    console.log("收到Chat:",event.data );
  });
}

onMounted(() => {
  initSSEConnection();
})

onUnmounted(() => {
  if (eventSource) {
    eventSource.close();
    eventSource = null;
  }
})

</script>

<template>
  <div>
    <a href="https://vite.dev" target="_blank">
      <img src="/vite.svg" class="logo" alt="Vite logo" />
    </a>
    <a href="https://vuejs.org/" target="_blank">
      <img src="./assets/vue.svg" class="logo vue" alt="Vue logo" />
    </a>
  </div>
  <HelloWorld msg="Vite + Vue" />
</template>

<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
