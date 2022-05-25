var App = new Vue({
    el: '#app',
    data: {
        products: [],
        selectedProduct: null,
        newStock: {
            productId: 0,
            description: "size",
            num: 10
        }
    },
    mounted() {
        this.getStock();
    },
    methods: {
        getStock() {
            this.loading = true;
            axios.get('/Admin/stocks').
                then(res => {
                    console.log(res);
                    this.products = Object.values(res.data)[1];
                })
                .catch(err => {
                    console.log(err.response);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        createStock() {
            this.loading = true;
            axios.post('/Admin/stocks', this.newStock).
                then(res => {
                    console.log(res);
                    this.products.stock.push(res.data);
                })
                .catch(err => {
                    console.log(err.response);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        updateStock() {
           this.loading = true;

        //    axios.put('/Admin/products', this.productModel)
        //        .then(res => {
        //            console.log(res.data);
        //            this.products.splice(this.objectIndex, 1, res.data);
        //        })
        //        .catch(err => {
        //            console.log(err);
        //        })
        //        .then(() => {
        //            this.loading = false;
        //            this.editing = false;
        //        });
        },
        selectProduct(product) {
            this.selectedProduct = product;
            this.newStock.productId = product.id //id
        },

    }


});