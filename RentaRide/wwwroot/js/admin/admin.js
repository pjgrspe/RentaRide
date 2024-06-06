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

        showLoader();  // Show the loader

        fetch(url)
            .then(response => response.text())
            .then(data => {
                mainContent.innerHTML = data;
                initChart();
                loadContent();
                initMapBoxIfNeeded(); // Initialize Mapbox after content is loaded
                hideLoader();  // Hide the loader
            })
            .catch(error => {
                console.error('Error loading content:', error);
                hideLoader();  // Hide the loader even if there's an error
            });

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

        showLoader();  // Show the loader

        fetch(url)
            .then(response => response.text())
            .then(data => {
                mainContent.innerHTML = data;
                initChart();
                loadContent();
                initMapBoxIfNeeded(); // Initialize Mapbox after content is loaded
                hideLoader();  // Hide the loader
            })
            .catch(error => {
                console.error('Error loading content:', error);
                hideLoader();  // Hide the loader even if there's an error
            });
    }

    // Sidebar toggle functionality
    document.querySelector("#sidebarCollapse").addEventListener("click", function () {
        document.querySelector("#sidebar").classList.toggle("active");
        this.classList.toggle("active");
    });

});

function initChart() {
    var ctx = document.getElementById('myChart').getContext('2d');

    var chartData = {
        labels: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"],
        datasets: [
            {
                label: "Orders",
                backgroundColor: "rgba(250, 184, 106, 0.8)",  // #fab86a with 50% opacity
                borderColor: "#fab86a",
                borderWidth: 1,
                data: [12, 14, 8, 16, 10, 9, 13]  // Data below 20
            }
        ]
    };

    var myChart = new Chart(ctx, {
        type: 'bar',
        data: chartData,
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                x: {
                    ticks: {
                        color: '#ffffff'  // Light color for x-axis labels
                    }
                },
                y: {
                    beginAtZero: true,
                    max: 20,  // Set maximum value for y-axis
                    ticks: {
                        color: '#ffffff'  // Light color for y-axis labels
                    }
                }
            },
            plugins: {
                legend: {
                    labels: {
                        color: '#ffffff'  // Light color for legend labels
                    }
                },
                tooltip: {
                    titleColor: '#ffffff',  // Light color for tooltip titles
                    bodyColor: '#ffffff',   // Light color for tooltip body
                    backgroundColor: 'rgba(0, 0, 0, 0.8)'  // Dark background for tooltip
                }
            }
        }
    });
}

let map; // Declare the map variable in the outer scope

function initMapBox() {
    mapboxgl.accessToken = 'pk.eyJ1IjoicGpncnNwZSIsImEiOiJjbHgyeTRld3UwYnd5MmpxNDNob3ZhMDN3In0.BgzV_qvJPTkUfmgK2c0aYw';

    carLocationPins();

    map.on('idle', function () {
        map.resize()
    })
}

function carLocationPins() {
    const geojson = {
        'type': 'FeatureCollection',
        'features': [
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Point',
                    'coordinates': [120.6010, 15.1456] // Angeles, Pampanga
                },
                'properties': {
                    'title': 'Angeles Center',
                    'description': 'Angeles, Pampanga'
                }
            },
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Point',
                    'coordinates': [120.6110, 15.1400] // Random point in Angeles, Pampanga
                },
                'properties': {
                    'title': 'Car 1',
                    'description': 'Angeles, Pampanga'
                }
            },
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Point',
                    'coordinates': [120.5910, 15.1500] // Random point in Angeles, Pampanga
                },
                'properties': {
                    'title': 'Car 2',
                    'description': 'Angeles, Pampanga'
                }
            },
            {
                'type': 'Feature',
                'geometry': {
                    'type': 'Point',
                    'coordinates': [120.6200, 15.1600] // Random point in Angeles, Pampanga
                },
                'properties': {
                    'title': 'Car 3',
                    'description': 'Angeles, Pampanga'
                }
            }
        ]
    };

    map = new mapboxgl.Map({
        container: 'map', // container id
        style: 'mapbox://styles/mapbox/standard', // stylesheet location
        center: [120.6010, 15.1456], // starting position [lng, lat] (Angeles, Pampanga)
        zoom: 13 // starting zoom
    });

    // add markers to map
    for (const feature of geojson.features) {
        // create a HTML element for each feature
        const el = document.createElement('div');
        el.className = 'marker';

        // make a marker for each feature and add it to the map
        new mapboxgl.Marker(el)
            .setLngLat(feature.geometry.coordinates)
            .setPopup(
                new mapboxgl.Popup({ offset: 25 }) // add popups
                    .setHTML(
                        `<h3>${feature.properties.title}</h3><p>${feature.properties.description}</p>`
                    )
            )
            .addTo(map);
    }

    // Force map resize
    map.resize();

    // Resize the map when the window is resized
    window.addEventListener('resize', () => {
        map.resize();
    });
}

function redirectToOrdersTab() {
    const ordersTabUrl = '/Admin/LoadPartial?tabName=Orders'; // Replace with the actual URL of your orders tab
    const ordersTabLink = document.querySelector(`.nav-item-link[href="${ordersTabUrl}"]`);

    if (ordersTabLink) {
        // Create a new event and call handleNavLinkClick with it
        const event = new MouseEvent('click', {
            view: window,
            bubbles: true,
            cancelable: true
        });
        ordersTabLink.dispatchEvent(event);
    }
}

function initMapBoxIfNeeded() {
    const mapContainer = document.getElementById('map');
    if (mapContainer) {
        initMapBox();

        // Force map resize after a slight delay to ensure correct dimensions
        setTimeout(() => {
            map.resize();
        }, 200);
    }
}

function reloadActivePartialView(message) {
    const activeLink = document.querySelector('.nav-item-link.active');
    if (activeLink) {
        const url = activeLink.getAttribute('href');

        showLoader();  // Show the loader

        fetch(url)
            .then(response => response.text())
            .then(data => {
                document.querySelector('.main-content').innerHTML = data;
                initChart();
                initMapBoxIfNeeded(); // Initialize Mapbox after content is loaded
                loadContent();
                toastSuccess(message);
                hideLoader();  // Hide the loader
            })
            .catch(error => {
                console.error('Error loading content:', error);
                hideLoader();  // Hide the loader even if there's an error
                toastSuccess(message);
            });
    }
}

function reloadDetailsContainer(message) {
    const carId = $('#logCarID').val();
    console.log('Reloading details for car ID:', carId);

    showLoader();  // Show the loader

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
            hideLoader();  // Hide the loader
        })
        .catch(error => {
            console.error('Error loading details:', error);
            hideLoader();  // Hide the loader even if there's an error
        });
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
    }, 300); // 300ms delay
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

function ordersFilterTable() {
    const searchInput = document.getElementById('searchInput').value.toLowerCase();
    const statusFilter = document.getElementById('statusFilter').value;
    const statusFilter2 = document.getElementById('statusFilter-2').value;

    // Get all tables to filter
    const tables = document.querySelectorAll('.table-container');

    tables.forEach(table => {
        const tableRows = table.querySelectorAll('.table-row');
        let rowsVisible = false;
        tableRows.forEach(row => {
            const name = row.children[1].innerText.toLowerCase();
            //ORDERS
            const paymentStatusClass = row.children[6].classList;
            const orderStatusClass = row.children[9].classList;
            let paymentStatus = '';
            let orderStatus = '';

            paymentStatusClass.forEach(className => {
                if (className.startsWith('status-')) {
                    paymentStatus = className.split('-')[1];
                }
            });

            orderStatusClass.forEach(className => {
                if (className.startsWith('status-')) {
                    orderStatus = className.split('-')[1];
                }
            });

            const matchesSearch = name.includes(searchInput);
            const matchesPaymentStatus = !statusFilter || paymentStatus === statusFilter;
            const matchesOrderStatus = !statusFilter2 || orderStatus === statusFilter2;

            if (matchesSearch && matchesPaymentStatus && matchesOrderStatus) {
                row.style.display = '';
                rowsVisible = true;
            } else {
                row.style.display = 'none';
            }
        });

        // Display "No results" message if no rows are visible in this table
        const noResultsMessage = table.querySelector('.no-results');
        if (!rowsVisible) {
            noResultsMessage.style.display = 'block';
        } else {
            noResultsMessage.style.display = 'none';
        }
    });
}

function filterTable() {
    const searchInput = document.getElementById('searchInput').value.toLowerCase();
    const statusFilter = document.getElementById('statusFilter').value;

    // Get all tables to filter
    const tables = document.querySelectorAll('.table-container');
    let rowsVisible = false;
    let cardsVisible = false;

    tables.forEach(table => {
        const tableRows = table.querySelectorAll('.table-row');
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
            } else {
                row.style.display = 'none';
            }
        });

        // Display "No results" message if no rows are visible in this table
        const noResultsMessage = table.querySelector('.no-results');
        if (!rowsVisible) {
            noResultsMessage.style.display = 'block';
        } else {
            noResultsMessage.style.display = 'none';
        }
    });

    // Filter card elements
    const cards = document.querySelectorAll('#cards-table .card-item');
    cards.forEach(card => {
        const name = card.querySelector('.card-heading')?.innerText.toLowerCase() || '';
        const listingName = card.querySelector('.card-heading-listing')?.innerText.toLowerCase() || '';
        const description = card.querySelector('.listing-description')?.innerText.toLowerCase() || '';
        const textDescription = card.querySelector('.card-text-description')?.innerText.toLowerCase() || '';
        const statusClass = card.querySelector('.status').classList;
        let status = '';

        statusClass.forEach(className => {
            if (className.startsWith('status-')) {
                status = className.split('-')[1];
            }
        });

        const matchesSearch = name.includes(searchInput) || listingName.includes(searchInput) || description.includes(searchInput) || textDescription.includes(searchInput);
        const matchesStatus = !statusFilter || status === statusFilter;

        if (matchesSearch && matchesStatus) {
            card.style.display = '';
            cardsVisible = true;
        } else {
            card.style.display = 'none';
        }
    });

    // Display "No results" message if no rows or cards are visible
    const noResultsMessageCards = document.getElementById('no-results');
    if (!rowsVisible && !cardsVisible) {
        noResultsMessageCards.style.display = 'block';
    } else {
        noResultsMessageCards.style.display = 'none';
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

/* ---------------------------------------------------
    MODAL SCRIPTS
----------------------------------------------------- */
function removeBackdrops() {
    $('.modal-backdrop').remove();
    $('body').removeClass('modal-open');
    $('body').css('padding-right', ''); // remove any padding added by Bootstrap to the body
}
