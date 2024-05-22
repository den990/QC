function getUserData() {
    return {
        login: 'den9900',
        password: 'den9900',
    }
}

function getAuthorizationUrl() {
    return '/user/login'
}

function getMessageText() {
    return 'Вы успешно авторизованы'
}

const data = {
    user: getUserData(),
    authorizationUrl: getAuthorizationUrl(),
    messageText: getMessageText(),
}

export {
    data,
}
