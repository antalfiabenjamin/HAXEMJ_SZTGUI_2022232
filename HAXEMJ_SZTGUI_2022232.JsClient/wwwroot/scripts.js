let phones = [];
let connection = null;

let phoneIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:29971/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("PhoneCreated", (user, message) => {
        getdata();
    });

    connection.on("PhoneDeleted", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function getdata() {
    await fetch('http://localhost:29971/phone')
        .then(x => x.json())
        .then(y => {
            phones = y;
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
    phones.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update</button>` +
                "</td></tr>";
    });
}

function showupdate(id) {
    document.getElementById('phonenametoupdate').value = phones.find(t => t['id'] == id)['name'];
    document.getElementById('updateformdiv').style.display = 'flex';
    phoneIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('phonenametoupdate').value;
    fetch('http://localhost:29971/phone', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                Name: name, id: phoneIdToUpdate
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
    let name = document.getElementById('phonename').value;
    fetch('http://localhost:29971/phone', {
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
    fetch('http://localhost:29971/phone/' + id, {
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