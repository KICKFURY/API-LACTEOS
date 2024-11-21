import { POST_Backup, POST_RestoreBackup } from '../endpoints.js'
import { POST } from '../generic-functions.js'
import { Alerta } from '../components/alert.js'

function AddEvents() {
    document.getElementById('btnRespaldar').addEventListener('click', Backup) 
    document.getElementById('btnRestaurar').addEventListener('click', Restore)
}

function Backup() {
    let backup = document.getElementById('txtBackup').value

    if (backup === '') {
        Alerta('Error', 'No se ha ingresado el nombre del respaldo', "error")
        return
    }

    POST(`${POST_Backup}${backup.split('\\')[2]}`, "Copia de seguridad creada correctamente", "Error al crear la copia de seguridad", () => {
        Alerta("Confirmado", "Copia de seguridad creada correctamente", "success")
    })
}

function Restore() {
    let restore = document.getElementById('txtRestore').value

    if (restore === '') {
        Alerta('Error', 'No se ha ingresado el nombre del respaldo', "error")
        return
    }

    POST(`${POST_RestoreBackup}${restore.split('\\')[2]}`, "Copia de seguridad restaurada correctamente", "Error al restaurar la copia de seguridad", () => {
        Alerta("Confirmado", "Copia de seguridad restaurada correctamente", "success")
    })
}

export { AddEvents }