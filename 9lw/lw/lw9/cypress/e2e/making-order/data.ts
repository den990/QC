function getProductUrl() {
    return '/product/casio-ga-1000-1aer'
}

function getUserData() {
    return {
        login: {
            busy: 'den9900',
            free: 'den9900',
        },
        password: 'den9900',
        name: 'den9900',
        email: 'den9900@mail.ru',
        address: 'Yoshkar-Ola, 100',
        note: 'Оставить у двери',
    }
}

function getPlaceOrderButtonText() {
    return 'Оформить заказ'
}

function getErrorLoginMessageText() {
    return 'Этот логин уже занят'
}


const data = {
    productUrl: getProductUrl(),
    user: getUserData(),
    placeOrderButtonText: getPlaceOrderButtonText(),
    errorLoginMessageText: getErrorLoginMessageText()
}

export {
    data,
}
