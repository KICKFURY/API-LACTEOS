function encriptar(value) {
    return CryptoJS.MD5(value).toString(CryptoJS.enc.Base64);
}

export { encriptar }
