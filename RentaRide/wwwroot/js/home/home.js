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


/*TABLE SCRIPTS*/
function filterTable() {
    const searchInput = document.getElementById('searchInput').value.toLowerCase();
    const statusFilter = document.getElementById('statusFilter').value;
    const tableRows = document.querySelectorAll('#applicationsTable .table-row');

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
    const table = document.getElementById("applicationsTable");
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

function openModal(id, name, email, contact, status) {
    $('#modalUserId').text(id);
    $('#modalUserName').text(name);
    $('#modalUserEmail').text(email);
    $('#modalUserContact').text(contact);
    $('#modalUserStatus').text(status);

    // Additional placeholders for demo purposes
    $('#modalDateCreated').text("May 20, 2024");
    $('#modalDateModified').text("May 21, 2024");
    $('#modalDateOfBirth').text("January 01, 1990");
    $('#modalStreetAddress').text("123 Main St");
    $('#modalCity').text("Sample City");
    $('#modalProvince').text("Sample Province");
    $('#modalDriversLicense').text("DL Placeholder");
    $('#modalSecondaryID').text("ID Placeholder");
    $('#modalProofOfBilling').text("Billing Placeholder");
    $('#modalSelfieWithID').text("Selfie Placeholder");

    $('#userModal').modal('show');
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