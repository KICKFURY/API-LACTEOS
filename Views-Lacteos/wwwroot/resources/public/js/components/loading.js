function loading() {
    setTimeout(() => {
        document.getElementById("loader").style.display = "none";
        document.getElementById("content").style.display = "block";
    }, 1000);
}

export { loading }