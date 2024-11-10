function GET(url, mensageError, optionResponseType, callbackThen, callbackCatch) {
    fetch(url)
        .then(response => optionResponseType == 1 ? response.json() : response.text())
        .then(data => {
            callbackThen(data)
        })
        .catch((error) => {
            callbackCatch(error)
            console.log(mensageError, error)
        })
}

function GET_SERVIDOR(url, mensageError, callback) {
    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const contentType = response.headers.get('content-type');
            if (!contentType || !contentType.includes('application/json')) {
                return response.text().then(text => { throw new TypeError(`Expected JSON, got ${text}`); });
            }
            return response.json();
        })
        .then(data => {
            callback(data)
        })
        .catch(error => console.log(mensageError, error))
}

function POST(url, mensageOK, mensageError, callback) {
    fetch(url,
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
        })
        .then(response => response.json())
        .then(data => {
            Swal.fire({
                title: 'Confirmado',
                text: mensageOK,
                icon: 'success',
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#7a2a1e',
            });
            console.log(`${mensageOK}`, data)
            callback()
        })
        .catch(error => console.log(`${mensageError}`, error))
}

function PUT(url,  mensageOK, mensageError, callback) {

    fetch(`${url}`, 
        {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response =>  response.json())
        .then(data => {
            console.log(mensageOK, data)
            callback()
        })
        .catch(error => console.log(mensageError, error))
}

function DELETE(url,  mensageOK, mensageError, callback) {
    fetch(url,
        {
            method:  'DELETE',
            headers: {
                'Content-Type': 'application/json'
            },
        })
        .then(response => response.json())
        .then(data => {
            console.log(mensageOK, data)
            callback()
        })
        .catch(error => console.log(mensageError, error))
}

export  { GET, GET_SERVIDOR, POST, PUT, DELETE };