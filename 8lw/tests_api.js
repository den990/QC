const axios = require('axios');
const assert = require('assert');
const Ajv = require('ajv');
const testDataCreate = require('./tests_create');
const testDataUpdate = require('./tests_update');

const productSchema = {
    type: 'object',
    properties: {
        id: {type: 'string'},
        category_id: {type: 'string'},
        title: {type: 'string'},
        alias: {type: 'string'},
        content: {type: 'string'},
        price: {type: 'string'},
        old_price: {type: 'string'},
        status: {type: 'string'},
        keywords: {type: 'string'},
        description: {type: 'string'},
        img: {type: 'string'},
        hit: {type: 'string'},
        cat: {type: 'string'}
    },
    required: ['id', 'category_id', 'title', 'alias', 'content', 'price', 'old_price', 'status', 'keywords', 'description', 'img', 'hit', 'cat'],
    additionalProperties: false
};
const ajv = new Ajv();
const validate = ajv.compile(productSchema);

const BASE_URL = 'http://shop.qatl.ru/api';
let saveId = [];
describe('Product API Tests', function () {
    it('should add a new product', async function () {
        this.timeout(100000);
        const createProductPromises = testDataCreate.createProductData.map(async (productData) => {
            const response = await axios.post(`${BASE_URL}/addproduct`, productData);
            assert.strictEqual(response.status, 200);
            assert.strictEqual(response.data.status, 1);
            saveId.push(response.data.id);

            const allProductsResponse = await axios.get(`${BASE_URL}/products`);
            const allProducts = allProductsResponse.data;
            const createdProduct = allProducts.find(product => product.id == response.data.id);
            const valid = validate(createdProduct);
            assert.strictEqual(valid, true);

            return response.data.id;
        });
        const productIds = await Promise.all(createProductPromises);
        productIds.sort();
        const allProductsResponse = await axios.get(`${BASE_URL}/products`);
        assert.strictEqual(allProductsResponse.status, 200);
        const productIdsFromResponse = allProductsResponse.data.map(product => product.id);
        const productIdsFromResponseNumbers = productIdsFromResponse.map(Number);
        assert.deepStrictEqual(productIds, productIdsFromResponseNumbers.slice(-productIds.length));


        const badCreateProductPromises = testDataCreate.badCreateProductData.map(async (badProductData) => {
            const response = await axios.post(`${BASE_URL}/addproduct`, badProductData);
            assert.strictEqual(response.status, 200);
            assert.strictEqual(response.data.status, 1);

            const allProductsResponse = await axios.get(`${BASE_URL}/products`);
            assert.strictEqual(allProductsResponse.status, 200);

            const productIds = allProductsResponse.data.map(product => product.id);
            assert.notStrictEqual(Number(productIds.slice(-1)[0]), response.data.id);
        });

        await Promise.all(badCreateProductPromises);
    });

    it('should update an existing product', async function () {
        this.timeout(100000);
        for (let i = 0; i < testDataUpdate.updateProductData.length; i++) {
            testDataUpdate.updateProductData[i] = {id: saveId[i], ...testDataUpdate.updateProductData[i]};
            const response = await axios.post(`${BASE_URL}/editproduct`, testDataUpdate.updateProductData[i]);
            assert.strictEqual(response.status, 200);
            assert.strictEqual(response.data.status, 1);

        }

        for (let i = 2; i < testDataUpdate.badUpdateProductData.length; i++) {
            testDataUpdate.badUpdateProductData[i] = {id: saveId[i], ...testDataUpdate.badUpdateProductData[i]};
            const response = await axios.post(`${BASE_URL}/editproduct`, testDataUpdate.badUpdateProductData[i]);
            assert.strictEqual(response.status, 200);
            assert.strictEqual(response.data.status, 1);

        }

        const allProductsResponse = await axios.get(`${BASE_URL}/products`);
        const allProducts = allProductsResponse.data;
        for (let i = 0; i < 2; i++) {
            const updatedProduct = allProducts.find(product => product.id == saveId[i]);
            assert.strictEqual(updatedProduct.keywords, 'updated, test, product');

            const valid = validate(updatedProduct);
            assert.strictEqual(valid, true);

        }
    });

    it('should delete an existing product', async function () {
        this.timeout(100000);
        for (let i = 0; i < 2; i++) {
            const response = await axios.get(`${BASE_URL}/deleteproduct?id=${saveId[i]}`);
            assert.strictEqual(response.status, 200);
            assert.strictEqual(response.data.status, 1);
        }
        let notSaveId = [783524682462, 82378349749822, -2373823];
        for (let i = 0; i < notSaveId.length; i++) {
            const response = await axios.get(`${BASE_URL}/deleteproduct?id=${notSaveId[i]}`);
            assert.strictEqual(response.status, 200);
            assert.strictEqual(response.data.status, 0);
            //черещ get смотреть продукты
        }
    });

    it('should get products', async function () {
        const response = await axios.get(`${BASE_URL}/products`);
        assert.strictEqual(response.status, 200);
        assert.notStrictEqual(response.data, null);
        let product = response.data[response.data.length - 1];
        const valid = validate(product);
        assert.strictEqual(valid, true);
    });
});
