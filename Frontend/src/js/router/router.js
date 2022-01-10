import VDecision from '../../views/VDecision.vue';
import VObject from '../../views/VObject.vue';
export default class Router{
    routes = [
        {path: '/', redirect: {name: 'decision'}},
        {path: '/Object/:id', name: 'object', component: VObject},
        {path: '/Decision/:id', name: 'decision', component: VDecision},
    ];
} 