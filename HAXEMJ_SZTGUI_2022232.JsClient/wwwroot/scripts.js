let type = document.getElementById('maintitle').innerHTML;

let data = [];
let connection = null;

let itemIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:29971/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on(`${type}Created`, (user, message) => {
        getdata();
    });

    connection.on(`${type}Deleted`, (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function getdata() {
    await fetch(`http://localhost:29971/${type.toLowerCase()}/`)
        .then(x => x.json())
        .then(y => {
            data = y;
            display();
        });
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

function display() {
    document.getElementById('resultarea').innerHTML = "";
    data.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" +
            `<button type="button" onclick="remove(${String(t.id)})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update</button>` +
                "</td></tr>";
    });
}

function showupdate(id) {
    document.getElementById(`${type.toLowerCase()}nametoupdate`).value = data.find(t => t['id'] == id)['name'];
    document.getElementById('updateformdiv').style.display = 'flex';
    itemIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById(`${type.toLowerCase()}nametoupdate`).value;
    fetch(`http://localhost:29971/${type.toLowerCase()}/`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                Name: name, id: itemIdToUpdate
            }),
        })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function create() {
    let name = document.getElementById(`${type.toLowerCase()}name`).value;
    fetch(`http://localhost:29971/${type.toLowerCase()}/`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                Name: name
            }),
        })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function remove(id) {
    fetch(`http://localhost:29971/${type.toLowerCase()}/` + id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        body: null })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}