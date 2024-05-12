const axios = require('axios');
const assert = require('assert');
const Ajv = require('ajv');
const testDataCreate = require('./tests_create');
const testDataUpdate = require('./tests_update');

const productSchema = {
    type: 'object',
    properties: {
        id: { type: 'string' },
        category_id: { type: 'string' },
        title: { type: 'string' },
        alias: { type: 'string' },
        content: { type: 'string' },
        price: { type: 'string' },
        old_price: { type: 'string' },
        status: { type: 'string' },
        keywords: { type: 'string' },
        description: { type: 'string' },
        img: { type: 'string' },
        hit: { type: 'string' },
        cat: { type: 'string' }
    },
    required: ['id', 'category_id', 'title', 'alias', 'content', 'price', 'old_price', 'status', 'keywords', 'description', 'img', 'hit', 'cat'],
    additionalProperties: false
};
const ajv = new Ajv();
const validate = ajv.compile(productSchema);

const BASE_URL = 'http://shop.qatl.ru/api';
let saveId = [];

describe('Product API Tests', function () {
    testDataCreate.createProductData.forEach((productData, index) => {
        it(`should add a new product ${index}`, async function () {
            this.timeout(100000);

            const response = await axios.post(`${BASE_URL}/addproduct`, productData);
            assert.strictEqual(response.status, 200);
            assert.strictEqual(response.data.status, 1);
            saveId.push(response.data.id);

            const allProductsResponse = await axios.get(`${BASE_URL}/products`);
            assert.strictEqual(allProductsResponse.status, 200);
            const allProducts = allProductsResponse.data;
            const createdProduct = allProducts.find(product => product.id == response.data.id);
            const valid = validate(createdProduct);
            assert.strictEqual(valid, true);
        });
    });

    testDataUpdate.updateProductData.forEach((productData, index) => {
        it(`should update an existing product ${index}`, async function () {
            this.timeout(100000);

            const updatedData = { id: saveId[index], ...productData };
            const response = await axios.post(`${BASE_URL}/editproduct`, updatedData);
            assert.strictEqual(response.status, 200);
            assert.strictEqual(response.data.status, 1);
        });
    });

    saveId.slice(0, 2).forEach((id, index) => {
        it(`should delete an existing product ${index}`, async function () {
            this.timeout(100000);

            const response = await axios.get(`${BASE_URL}/deleteproduct?id=${id}`);
            assert.strictEqual(response.status, 200);
            assert.strictEqual(response.data.status, 1);
        });
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
