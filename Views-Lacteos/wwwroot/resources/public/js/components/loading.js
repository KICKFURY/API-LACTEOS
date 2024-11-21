class Loader {
    constructor() {
        this.loaderElement = document.getElementById("loader");
        this.contentElement = document.getElementById("content");
    }

    show() {
        this.loaderElement.style.display = "block";
        this.contentElement.style.display = "none";
    }

    hide() {
        setTimeout(() => {
            this.loaderElement.style.display = "none";
            this.contentElement.style.display = "block";
        }, 1000);
    }
}

export default Loader