var app = new Vue({
    el: '#app', //entry to dumm element to vue will look for all the depinding
    data: {
        price: 0,
        showPrice: true,
    }, // all obj prop var within vue 
    methods: {
        togglePrice: function () {
            this.showPrice = !this.showPrice;
        },
        alert(v){
            alert(v);
        },
    },
    computed: {
        formatPrice: function (){
            return "$" + this.price
        }
    }
});
