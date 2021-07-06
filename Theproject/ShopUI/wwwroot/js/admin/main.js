﻿var app = new Vue({
    el: '#app', //entry to dumm element to vue will look for all the depinding
    data: {

        loading: false,

        products: []
    }, // all obj prop var within vue 
    methods: {
        getProducts() {
            this.loading = true;
            axios.get('/Admin/products').
                then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err.response);
                })
                .then(() => {
                    this.loading = false;
                });
        },

        createProduct() {
            this.loading = true;
            axios.post('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
    }
});
