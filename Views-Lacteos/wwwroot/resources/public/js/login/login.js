import { AddEvents } from './login-function.js'
import { loading } from '../components/loading.js'

window.onload = function () {
    AddEvents()
}

window.addEventListener('DOMContentLoaded', () => {
    loading()
})