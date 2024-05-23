/* ---------------------------------------------------
    SIDEBAR SCRIPTS
----------------------------------------------------- */
document.addEventListener("DOMContentLoaded", function () {
    const navLinks = document.querySelectorAll('.nav-item-link');
    const profileLinks = document.querySelectorAll('.profile-menu .dropdown-item');
    const mainContent = document.querySelector('.main-content');

    function setActiveTab(link) {
        // Remove active class from all nav links
        navLinks.forEach(nav => nav.classList.remove('active', 'fw-bold'));

        // Add active class to the specified link
        link.classList.add('active', 'fw-bold');
    }

    function handleNavLinkClick(event) {
        event.preventDefault(); // Prevent default link behavior

        setActiveTab(this);

        // Load the content dynamically
        const url = this.getAttribute('href');
        fetch(url)
            .then(response => response.text())
            .then(data => {
                mainContent.innerHTML = data;
            })
            .catch(error => console.error('Error loading content:', error));
    }

    function handleProfileLinkClick(event) {
        event.preventDefault(); // Prevent default link behavior

        // Determine which sidebar tab to activate based on the profile link clicked
        const profileMenuName = this.getAttribute('href').split('=')[1]; // Get the menuName parameter value
        const sidebarLinkToActivate = Array.from(navLinks).find(link => link.getAttribute('href').includes(profileMenuName));

        if (sidebarLinkToActivate) {
            setActiveTab(sidebarLinkToActivate);
        }

        // Load the content dynamically
        const url = this.getAttribute('href');
        fetch(url)
            .then(response => response.text())
            .then(data => {
                mainContent.innerHTML = data;
            })
            .catch(error => console.error('Error loading content:', error));
    }

    // Add event listeners to sidebar nav links
    navLinks.forEach(link => {
        link.addEventListener('click', handleNavLinkClick);
    });

    // Add event listeners to profile dropdown links
    profileLinks.forEach(link => {
        link.addEventListener('click', handleProfileLinkClick);
    });
});

$(document).ready(function () {
    $("#sidebarCollapse").on("click", function () {
        $("#sidebar").toggleClass("active");
        $(this).toggleClass("active");
    });
});

/* ---------------------------------------------------
    TABLE SCRIPTS
----------------------------------------------------- */
function filterTable() {
    const searchInput = document.getElementById('searchInput').value.toLowerCase();
    const statusFilter = document.getElementById('statusFilter').value;
    const tableRows = document.querySelectorAll('#usersTable .table-row');

    tableRows.forEach(row => {
        const name = row.children[1].innerText.toLowerCase();
        const statusClass = row.children[5].classList;
        let status = '';

        statusClass.forEach(className => {
            if (className.startsWith('status-')) {
                status = className.split('-')[1];
            }
        });

        const matchesSearch = name.includes(searchInput);
        const matchesStatus = !statusFilter || status === statusFilter;

        if (matchesSearch && matchesStatus) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}

let sortOrder = {};

function sortTable(columnIndex) {
    const table = document.getElementById("usersTable");
    const rows = Array.from(table.getElementsByClassName("table-row"));
    const ths = table.getElementsByTagName("th");

    let order = sortOrder[columnIndex] || 'asc';
    sortOrder[columnIndex] = order === 'asc' ? 'desc' : 'asc';

    rows.sort((a, b) => {
        let cellA = a.cells[columnIndex].innerText;
        let cellB = b.cells[columnIndex].innerText;

        if (columnIndex === 2) { // Date column
            cellA = new Date(cellA);
            cellB = new Date(cellB);
        } else if (columnIndex === 0) { // ID column
            cellA = parseInt(cellA);
            cellB = parseInt(cellB);
        }

        if (order === 'asc') {
            return cellA > cellB ? 1 : -1;
        } else {
            return cellA < cellB ? 1 : -1;
        }
    });

    table.innerHTML = '';
    rows.forEach(row => table.appendChild(row));

    updateIcons(columnIndex);
}

function updateIcons(columnIndex) {
    const headers = document.querySelectorAll(".sortable");
    headers.forEach((header, index) => {
        header.querySelector("i").className = "fas fa-sort ms-1";
        if (index === columnIndex) {
            header.querySelector("i").className = sortOrder[columnIndex] === 'asc' ? "fas fa-sort-up ms-1" : "fas fa-sort-down ms-1";
        }
    });
}


/* ---------------------------------------------------
    USER TABLE SCRIPTS
----------------------------------------------------- */
function openModalUserDetails(id, name, email, contact, status, dateCreated, dateModified, dateOfBirth, streetAddress, city, province, driversLicenseSrc, driversLicenseBackSrc, secondaryIDSrc, proofOfBillingSrc, selfiePicSrc) {
    $('#modalUserId').text(id);
    $('#modalUserName').text(name);
    $('#modalUserEmail').text(email);
    $('#modalUserContact').text(contact);
    $('#modalUserStatus').text(status);

    // Set additional modal data
    $('#modalDateCreated').text(dateCreated);
    $('#modalDateModified').text(dateModified);
    $('#modalDateOfBirth').text(dateOfBirth);
    $('#modalStreetAddress').text(streetAddress);
    $('#modalCity').text(city);
    $('#modalProvince').text(province);

    // Set the src attribute for the images
    $('#modalDriversLicense img').attr('src', driversLicenseSrc);
    $('#driversLicenseFrontImage').attr('src', driversLicenseSrc);
    $('#driversLicenseBackImage').attr('src', driversLicenseBackSrc);
    $('#modalSecondaryID img').attr('src', secondaryIDSrc);
    $('#secondaryIDModal img').attr('src', secondaryIDSrc);
    $('#modalProofOfBilling img').attr('src', proofOfBillingSrc);
    $('#proofOfBillingModal img').attr('src', proofOfBillingSrc);
    $('#modalSelfieWithID img').attr('src', selfiePicSrc);
    $('#selfieWithIDModal img').attr('src', selfiePicSrc);
    document.getElementById('approveUserId').value = id;
    document.getElementById('denyUserId').value = id;

    $('#userModal').modal('show');
}

function closeModalUserDetails() {
    $('#userModal').modal('hide');
}

function approveUser() {
    // Implement approval logic here
    alert("User approved.");
    $('#userModal').modal('hide');

}

function denyUser() {
    // Implement denial logic here
    alert("User denied.");
    $('#userModal').modal('hide');
}


/* ---------------------------------------------------
    DRIVERS TABLE SCRIPT
----------------------------------------------------- */

/*ADD DRIVER MODAL*/
function openModalAddDriver() {
    $('#addNewDriverModal').modal('show');
}

function closeModalAddDriver() {
    $('#addNewDriverModal').modal('hide');
}

function addDriver() {
    alert("User added.");
    $('#addNewDriverModal').modal('hide');
}



/*EDIT DRIVER MODAL*/
function openModalEditDriver(firstname,middlename,lastname,email,contact,status) {
    $('#driverFirstName-edit').val(firstname);
    $('#driverMiddleName-edit').val(middlename);
    $('#driverLastName-edit').val(lastname);
    $('#driverEmail-edit').val(email);
    $('#driverContact-edit').val(contact);
    $('#driverStatus-edit').val(status);

    $('#editDriverModal').modal('show');
}

function closeModalEditDriver() {
    $('#editDriverModal').modal('hide');
}

function editDriver(){
    alert("User edited.");
    $('#editDriverModal').modal('hide');
}



/*CONTACT MODAL*/
function openModalDriverContact(name, email, contact) {
    $('#modalUserName').text(name);
    $('#modalUserEmail').text(email);
    $('#modalUserContact').text(contact);

    $('#contactModal').modal('show');
}

function closeModalDriverContact() {
    $('#contactModal').modal('hide');
}



/*DRIVER PICTURE MODAL*/
function openModalDriverPicture(driverPicture) {
    $('#driversPictureModal img').attr('src', driverPicture);

    $('#driversPictureModal').modal('show');
}

function closeModalDriverPicture() {
    $('#driversPictureModal').modal('hide');
}



/*VIEW DRIVER'S LICENSE MODAL*/
function openModalViewDriverLicense(front,back) {
    $('#driversLicenseFrontImage').attr('src', front);
    $('#driversLicenseBackImage').attr('src', back);

    $('#driversLicenseModal').modal('show');
}

function closeModalViewDriverLicense() {
    $('#driversLicenseModal').modal('hide');
}


/*DELETE DRIVER MODAL*/
function openModalDeleteDriver() {
    $('#deleteDriverModal').modal('show');
}

function closeModalDeleteDriver() {
    $('#deleteDriverModal').modal('hide');
}

function deleteDriver() {
    alert("User deleted.");
    $('#deleteDriverModal').modal('hide');
}