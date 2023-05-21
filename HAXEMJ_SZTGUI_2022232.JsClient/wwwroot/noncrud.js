let data = []

async function GetAlliPhones() {
    await fetch('http://localhost:29971/Noncrud/GetAlliPhones')
        .then(x => x.json())
        .then(y => {
            data = y;
        });

    document.getElementById('theadarea').innerHTML = "";
    document.getElementById('theadarea').innerHTML = "<tr><th>ID</th><th>Name</th></tr>";
    document.getElementById('resultarea').innerHTML = "";
    data.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td></tr>"
    });
}

async function GetPreferredColorPhones() {
    let color = document.getElementById('noncrudinput').value;

    if (color != "") {
        await fetch(`http://localhost:29971/Noncrud/GetPreferredColorPhones/${color}`)
            .then(x => x.json())
            .then(y => {
                data = y;
            });

        document.getElementById('theadarea').innerHTML = "";
        document.getElementById('theadarea').innerHTML = "<tr><th>Name</th><th>Count</th></tr>";
        document.getElementById('resultarea').innerHTML = "";
        data.forEach(t => {
            document.getElementById('resultarea').innerHTML +=
                "<tr><td>" + t.name + "</td><td>" + t.colorPhoneCount + "</td></tr>"
        });
    } else {
        alert('Define a color please');
    }  
}

async function GetPhoneCountByUser() {
    await fetch('http://localhost:29971/Noncrud/GetPhoneCountByUser')
        .then(x => x.json())
        .then(y => {
            data = y;
        });

    document.getElementById('theadarea').innerHTML = "";
    document.getElementById('theadarea').innerHTML = "<tr><th>Name</th><th>Number of owned phones</th></tr>";
    document.getElementById('resultarea').innerHTML = "";
    data.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.name + "</td><td>" + t.phoneCount + "</td></tr>"
    });
}

async function GetScreenTimeBd() {
    await fetch('http://localhost:29971/Noncrud/GetScreenTimeBd')
        .then(x => x.json())
        .then(y => {
            data = y;
        });

    document.getElementById('theadarea').innerHTML = "";
    document.getElementById('theadarea').innerHTML = "<tr><th>Name</th><th>Total average screentime</th></tr>";
    document.getElementById('resultarea').innerHTML = "";
    data.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.name + "</td><td>" + t.totalAvgScrTime + "</td></tr>"
    });
}

async function PhonesBySpecificLocation() {
    let location = document.getElementById('noncrudinput').value;

    if (location != "") {
        await fetch(`http://localhost:29971/Noncrud/PhonesBySpecificLocation/${location}`)
            .then(x => x.json())
            .then(y => {
                data = y;
            });

        document.getElementById('theadarea').innerHTML = "";
        document.getElementById('theadarea').innerHTML = "<tr><th>ID</th><th>Name</th></tr>";
        document.getElementById('resultarea').innerHTML = "";
        data.forEach(t => {
            document.getElementById('resultarea').innerHTML +=
                "<tr><td>" + t.id + "</td><td>" + t.name + "</td></tr>"
        });
    } else {
        alert('Define a location please');
    }
}