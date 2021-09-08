var app = new Vue({
    el: '#app', //entry to dumm element to vue will look for all the depinding
    data: {

        loading: false,
        objectIndex: 0,
        productModel: {
            id: 0,
            name: "Name",
            description: "Description",
            price:"1.32"
        },
        products: []
    }, // all obj prop var within vue 
    mounted() {
        this.getProducts();
    },
    methods: {
        getProduct(id) {
            this.loading = true;
            axios.get('/Admin/products/' + id).
                then(res => {
                    console.log(res);
                   var product = res.data;
                    this.productModel = {
                        id: product.id,
                        name: product.name,
                        description: product.description,
                        price: product.price

                    }
                })
                .catch(err => {
                    console.log(err.response);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        getProducts() {
            this.loading = true;
            axios.get('/Admin/products').
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

        createProduct() {
            this.loading = true;

            axios.post('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        updateProduct() {
            this.loading = true;

            axios.put('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.splice(this.objectIndex,1,res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        deleteProduct(id, index) {
            this.loading = true;
            axios.delete('/Admin/products/' + id).
                then(res => {
                    console.log(res);
                    this.products.splice(index,1);
                })
                .catch(err => {
                    console.log(err.response);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        editProduct(id, index) {
            this.objectIndex = index;
            this.getProduct(id);
        }
    }
});
