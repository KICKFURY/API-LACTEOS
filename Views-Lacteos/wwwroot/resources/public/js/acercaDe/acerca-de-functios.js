import { acerca_de_vendedor } from "../endpoints.js"

function AddEvents() {
    document.getElementById('btnManual').addEventListener('click', () => {
        document.getElementById('manual1').src = acerca_de_vendedor
        window.manual.showModal()
    })

    document.getElementById('manual2').addEventListener('click', () => {
        window.manual.close()
    })
}

export { AddEvents }