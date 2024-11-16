import HTMLLoader from './HTMLLoader.js';

let htmlContent = localStorage.getItem('sidebarContent');

const getHTMLLoaderInstance = async (url) => {
    if (!htmlContent) {
        const htmlLoader = new HTMLLoader(url);
        htmlContent = await htmlLoader.loadHTML();
        localStorage.setItem('sidebarContent', htmlContent);
    } else {
        console.log('Cargando contenido desde localStorage');
    }
    return htmlContent;
};

export default getHTMLLoaderInstance;