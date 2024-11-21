import { Alerta } from './alert.js'

function copyToClipboard(text) {
    const textarea = document.createElement("textarea");
    textarea.value = text;
    document.body.appendChild(textarea);
    textarea.select();
    document.execCommand("copy");
    document.body.removeChild(textarea);
    Alerta("Confirmado", "Cedula Copiada", "success")
}

export { copyToClipboard };