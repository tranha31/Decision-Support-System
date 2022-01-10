import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'

Vue.config.productionTip = false
Vue.use(VueRouter);

import Router from '../src/js/router/router.js'
var route = new Router();
var routes = route.routes;
const router = new VueRouter({
  mode: 'history',
  base : process.env.BASE_URL,
  routes
})


new Vue({
  render: h => h(App),
  router,
  created(){
    if (performance.navigation.type == 1) {
      if(this.$router.path == '/') {
        this.$router.push({path : '/Decision/0'});
      }
    }
  }
}).$mount('#app')
