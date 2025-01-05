var App = new Vue({
    el: '#app',
    data: {
        products: [],
        selectedProduct: null,
        newStock: {
            productId: 0,
            description: "size",
            num: 10
        },
        stockList: null
    },
    mounted() {
        this.getStock();
    },
    methods: {
        getStock() {
            this.loading = true;
            axios.get('/stocks').
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
            axios.post('/stocks', this.newStock).
                then(res => {
                    console.log(res);
                    Object.values(this.selectedProduct.stock)[1].push(res.data);
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

            axios.put('/stocks', {
                stock: Object.values(this.selectedProduct.stock)[1].map(x => {
                    return {
                        id: x.id,
                        description: x.description,
                        num: x.num,
                        productId: this.selectedProduct.id
                    };
                })
            })
                .then(res => {
                    console.log(res.data);
                    this.products.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        deleteStock(id, index) {
           this.loading = true;
            axios.delete('/stocks/'+ id)
                .then(res => {
                    console.log(res);
                    Object.values(this.selectedProduct.stock)[1].splice(index,1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        selectProduct(product) {
            this.selectedProduct = product;
            this.newStock.productId = product.id 
        },
    }


});