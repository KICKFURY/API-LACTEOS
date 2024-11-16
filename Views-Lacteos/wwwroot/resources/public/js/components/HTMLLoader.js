class HTMLLoader {
    constructor(url) {
        this.url = url;
        this.content = null;
    }

    async loadHTML() {
        const response = await fetch(this.url);
        this.content = await response.text();
        return this.content;
    }
}

export default HTMLLoader;