function GET(url, mensageError, callback) {
    fetch(url)
        .then(response => response.json())
        .then(data => {
            callback(data)
        })
        .catch(error => console.log(mensageError, error))
}

function GETInterno(url, mensageError, callback) {
    fetch(url)
        .then(response => response.text())
        .then(data => {
            callback(data)
        })
        .catch(error => console.log(mensageError, error))
}

function GETTABLARUC(url, mensageError, callback) {
    fetch(url)
    .then(response => response.json())
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

export  { GET, GETTABLARUC, GETInterno, POST, PUT, DELETE };