function encriptar(value) {
    return CryptoJS.MD5(value).toString(CryptoJS.enc.Base64);
}

function preventSourceCode() {
    document.addEventListener("keydown", function (event) {
        if (
            (event.ctrlKey && event.key === "u") ||
            (event.ctrlKey && event.key === "w")
        ) {
            event.preventDefault();
        } else if (event.key === "F12") {
            console.log(event.key);
            event.preventDefault();
        }
    });

    document.addEventListener('contextmenu', function(event) {
        event.preventDefault(); 
    });
}

export { encriptar, preventSourceCode }
