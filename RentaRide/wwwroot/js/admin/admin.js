/* ---------------------------------------------------
   SIDEBAR SCRIPTS
----------------------------------------------------- */
document.addEventListener("DOMContentLoaded", function () {
    const navLinks = document.querySelectorAll('.nav-item-link');
    const profileLinks = document.querySelectorAll('.profile-menu .dropdown-item');
    const mainContent = document.querySelector('.main-content');
    const dashboardTab = document.querySelector('.nav-item-link[href="/Admin/LoadPartial?tabName=Dashboard"]'); // Adjust the selector as needed

    function setActiveTab(link) {
        // Remove active class from all nav links
        navLinks.forEach(nav => nav.classList.remove('active', 'fw-bold'));

        // Add active class to the specified link
        link.classList.add('active', 'fw-bold');

        // Save the active tab to local storage
        localStorage.setItem('activeTab', link.getAttribute('href'));
    }

    function handleNavLinkClick(event) {
        event.preventDefault();

        // Fetch the content dynamically when a nav link is clicked
        const url = event.currentTarget.getAttribute('href');
        fetch(url)
            .then(response => response.text())
            .then(data => {
                mainContent.innerHTML = data;
                loadContent();
            })
            .catch(error => console.error('Error loading content:', error));

        setActiveTab(event.currentTarget);
    }

    // Add click event listener to nav links
    navLinks.forEach(link => {
        link.addEventListener('click', handleNavLinkClick);
    });

    // Add click event listener to profile links
    profileLinks.forEach(link => {
        link.addEventListener('click', handleProfileLinkClick);
    });

    // Set the active tab on page load based on local storage or default to Dashboard
    const activeTabHref = localStorage.getItem('activeTab');
    const activeTab = activeTabHref ? Array.from(navLinks).find(link => link.getAttribute('href') === activeTabHref) : dashboardTab;

    if (activeTab) {
        setActiveTab(activeTab);

        // Load the content dynamically
        const url = activeTab.getAttribute('href');
        fetch(url)
            .then(response => response.text())
            .then(data => {
                mainContent.innerHTML = data;
                loadContent();
            })
            .catch(error => console.error('Error loading content:', error));
    }

    // Sidebar toggle functionality
    document.querySelector("#sidebarCollapse").addEventListener("click", function () {
        document.querySelector("#sidebar").classList.toggle("active");
        this.classList.toggle("active");
    });
});



function reloadActivePartialView(message) {
    const activeLink = document.querySelector('.nav-item-link.active');
    if (activeLink) {
        const url = activeLink.getAttribute('href');
        fetch(url)
            .then(response => response.text())
            .then(data => {
                document.querySelector('.main-content').innerHTML = data;
                loadContent();
                toastSuccess(message);
            })
            .catch(error => {
                toastSuccess(message);
                console.error('Error loading content:', error);
            });
    }
}

function reloadDetailsContainer(message) {
    const carId = $('#logCarID').val();
    console.log('Reloading details for car ID:', carId);

    fetch(`/Admin/GetCarDetails?carId=${carId}&forDetailsPage=true`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.text();
        })
        .then(data => {
            console.log('Received data:', data);
            document.querySelector('#detailsContainer').innerHTML = data;
            document.querySelector('#detailsContainer').style.display = 'block';
            toastSuccess(message);
        })
        .catch(error => console.error('Error loading details:', error));
}


/* ---------------------------------------------------
    LOADER SCRIPTS
----------------------------------------------------- */

function showLoader() {
    const loader = document.getElementById('loader');
    const tableContent = document.getElementById('tableContent');

    if (loader && tableContent) {
        loader.classList.remove('d-none');
        tableContent.classList.add('d-none');
    }
}

function hideLoader() {
    setTimeout(() => {
        const loader = document.getElementById('loader');
        const tableContent = document.getElementById('tableContent');

        if (loader && tableContent) {
            loader.classList.add('d-none');
            tableContent.classList.remove('d-none');
        }
    }, 300); // 3ms delay
}

function loadContent() {
    showLoader();
    hideLoader();
}

/* ---------------------------------------------------
    TOAST SCRIPTS
----------------------------------------------------- */

function toastSuccess(message) {
    const Toast = Swal.mixin({
        toast: true,
        position: "top",
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.onmouseenter = Swal.stopTimer;
            toast.onmouseleave = Swal.resumeTimer;
        }
    });
    Toast.fire({
        icon: "success",
        title: message
    });
}

function toastError(message) {
    const Toast = Swal.mixin({
        toast: true,
        position: "top",
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.onmouseenter = Swal.stopTimer;
            toast.onmouseleave = Swal.resumeTimer;
        }
    });
    Toast.fire({
        icon: "error",
        title: message
    });
}

/* ---------------------------------------------------
    TABLE SCRIPTS
----------------------------------------------------- */

function filterTable() {
    const searchInput = document.getElementById('searchInput').value.toLowerCase();
    const statusFilter = document.getElementById('statusFilter').value;

    // Filter table rows
    const tableRows = document.querySelectorAll('#usersTable .table-row');
    let rowsVisible = false;
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
            rowsVisible = true;
            loadContent();
        } else {
            row.style.display = 'none';
            loadContent();
        }
    });

    // Filter card elements
    const cards = document.querySelectorAll('#cards-table .card-item');
    let cardsVisible = false;
    cards.forEach(card => {
        const name = card.querySelector('.card-heading').innerText.toLowerCase();
        const description = card.querySelector('.card-text').innerText.toLowerCase();
        const statusClass = card.querySelector('.status').classList;
        let status = '';

        statusClass.forEach(className => {
            if (className.startsWith('status-')) {
                status = className.split('-')[1];
            }
        });

        const matchesSearch = name.includes(searchInput) || description.includes(searchInput);
        const matchesStatus = !statusFilter || status === statusFilter;

        if (matchesSearch && matchesStatus) {
            card.style.display = '';
            cardsVisible = true;
            loadContent();
        } else {
            card.style.display = 'none';
            loadContent();
        }
    });

    // Display "No results" message if no rows or cards are visible
    const noResultsMessage = document.getElementById('no-results');
    if (!rowsVisible && !cardsVisible) {
        noResultsMessage.style.display = 'block';
    } else {
        noResultsMessage.style.display = 'none';
    }
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