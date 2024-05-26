/* ---------------------------------------------------
   SIDEBAR SCRIPTS
----------------------------------------------------- */
document.addEventListener("DOMContentLoaded", function () {
    const navLinks = document.querySelectorAll('.nav-item-link');
    const profileLinks = document.querySelectorAll('.profile-menu .dropdown-item');
    const mainContent = document.querySelector('.main-content');
    const dashboardTab = document.querySelector('.nav-item-link[href="/Admin/LoadPartial?menuName=Dashboard"]'); // Adjust the selector as needed

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
    document.getElementById('userID-Verify').value = id;

    $('#userModal').modal('show');
}

function closeModalUserDetails() {
    $('#modalUserId').text('');
    $('#modalUserName').text('');
    $('#modalUserEmail').text('');
    $('#modalUserContact').text('');
    $('#modalUserStatus').text('');

    // Clear additional modal data
    $('#modalDateCreated').text('');
    $('#modalDateModified').text('');
    $('#modalDateOfBirth').text('');
    $('#modalStreetAddress').text('');
    $('#modalCity').text('');
    $('#modalProvince').text('');

    // Clear the src attribute for the images
    $('#modalDriversLicense img').attr('src', '');
    $('#driversLicenseFrontImage').attr('src', '');
    $('#driversLicenseBackImage').attr('src', '');
    $('#modalSecondaryID img').attr('src', '');
    $('#secondaryIDModal img').attr('src', '');
    $('#modalProofOfBilling img').attr('src', '');
    $('#proofOfBillingModal img').attr('src', '');
    $('#modalSelfieWithID img').attr('src', '');
    $('#selfieWithIDModal img').attr('src', '');
    $('#userID-Verify').text('');

    $('#userModal').modal('hide');
}

//LUISSSSSSSSSSSSSSSSSSSSS FUCK EM UP
//CODE OPTIMIZED
function setUserVerify(isVerified) {
    var formData = new FormData();
    formData.append('userverID', $('#userID-Verify').val());
    formData.append('userverIsVerified', isVerified);

    fetch('/Admin/UserVerify', {
        method: 'POST',
        body: formData
    })
        .then(response => response.text().then(text => text ? JSON.parse(text) : {}))
        .then(data => {
            if (data.success) {
                //SUCCESS
                if (isVerified == true) {
                    //JASON INSERT YOUR JS CODE HERE
                    alert("User approved.");
                }
                else if (isVerified == false) {
                    //JASON INSERT YOUR JS CODE HERE
                    alert("User denied.");
                }
                else {
                    //JASON INSERT YOUR JS CODE HERE
                    alert("User set to Pending.");

                }
            } else {
                //FAILED
                //reloadActivePartialView("Something went wrong.");
                //alert(data.message); <-- USE THIS IF YOU WANT TO DISPLAY THE MESSAGE FROM THE CONTROLLER
                alert("Something went wrong");
            }
            closeModalUserDetails();
        })
        .catch(error => console.error('Error:', error));
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
    var formData = new FormData();
    formData.append('drivmodelFirstName', $('#driverFirstName').val());
    formData.append('drivmodelMiddleName', $('#driverMiddleName').val());
    formData.append('drivmodelLastName', $('#driverLastName').val());
    formData.append('drivmodelEmail', $('#driverEmail').val());
    formData.append('drivmodelContact', $('#driverContact').val());
    formData.append('drivmodelImage', $('#driverPicture')[0].files[0]);
    formData.append('drivmodelLicense', $('#driverLicenseFront')[0].files[0]);
    formData.append('drivmodelLicenseBack', $('#driverLicenseBack')[0].files[0]);

    fetch('/Admin/AddNewDriver', {
        method: 'POST',
        body: formData
    })
        .then(response => response.text().then(text => text ? JSON.parse(text) : {}))
        .then(data => {
            if (data.success) {
                $('#addNewDriverModal').modal('hide');
                reloadActivePartialView("Driver successfully added.");
            } else {
                reloadActivePartialView("Something went wrong.");
            }
        })
        .catch(error => console.error('Error:', error));
}

/*EDIT DRIVER MODAL*/
function openModalEditDriver(id,firstname,middlename,lastname,email,contact,status) {
    $('#driverID-edit').val(id);
    $('#driverFirstName-edit').val(firstname);
    $('#driverMiddleName-edit').val(middlename);
    $('#driverLastName-edit').val(lastname);
    $('#driverEmail-edit').val(email);
    $('#driverContact-edit').val(contact);
    $('#driverStatus-edit').val(status);

    $('#editDriverModal').modal('show');
}

function closeModalEditDriver() {
    $('#driverID-edit').val('');
    $('#driverFirstName-edit').val('');
    $('#driverMiddleName-edit').val('');
    $('#driverLastName-edit').val('');
    $('#driverEmail-edit').val('');
    $('#driverContact-edit').val('');
    $('#driverStatus-edit').val('');

    $('#editDriverModal').modal('hide');
}

function editDriver() {
    var formData = new FormData();
    formData.append('driveditmodelID', $('#driverID-edit').val());
    formData.append('driveditmodelFirstName', $('#driverFirstName-edit').val());
    formData.append('driveditmodelMiddleName', $('#driverMiddleName-edit').val());
    formData.append('driveditmodelLastName', $('#driverLastName-edit').val());
    formData.append('driveditmodelEmail', $('#driverEmail-edit').val());
    formData.append('driveditmodelContact', $('#driverContact-edit').val());
    formData.append('driveditmodelStatus', $('#driverStatus-edit').val());
    formData.append('driveditmodelImage', $('#driverPicture-edit')[0].files[0]);
    formData.append('driveditmodelLicense', $('#driverLicenseFront-edit')[0].files[0]);
    formData.append('driveditmodelLicenseBack', $('#driverLicenseBack-edit')[0].files[0]);

    fetch('/Admin/EditDriver', {
        method: 'POST',
        body: formData
    })
        .then(response => response.text().then(text => text ? JSON.parse(text) : {}))
        .then(data => {
            //JASON INSERT YOUR JS CODE HERE
            if (data.success) {
                //SUCCESS
                closeModalEditDriver();
                reloadActivePartialView("Driver successfully edited.");
            } else {
                //FAILED
                reloadActivePartialView("Something went wrong.");
                //alert(data.message); <-- USE THIS IF YOU WANT TO DISPLAY THE MESSAGE FROM THE CONTROLLER
            }
            

        })
        .catch(error => console.error('Error:', error));
}

/*DELETE DRIVER MODAL*/
function openModalDeleteDriver(driverFullname, driverID) {
    $('#driverID-delete').val(driverID);

    // Set the driver's full name in the modal
    $('#deleteDriverName').text(`${driverFullname}?`);

    $('#deleteDriverModal').modal('show');
}
function closeModalDeleteDriver() {
    $('#driverID-delete').val('');
    $('#deleteDriverName').text('');

    $('#deleteDriverModal').modal('hide');
}

function deleteDriver() {
    var formData = new FormData();
    formData.append('drivdelmodelID', $('#driverID-delete').val());

    fetch('/Admin/DeleteDriver', {
        method: 'POST',
        body: formData
    })
        .then(response => response.text().then(text => text ? JSON.parse(text) : {}))
        .then(data => {
            //JASON INSERT YOUR JS CODE HERE
            if (data.success) {
                //SUCCESS
                //$('#deleteDriverModal').modal('hide');
                closeModalDeleteDriver();
                reloadActivePartialView("Driver successfully deleted.");
            } else {
                //FAILED
                reloadActivePartialView("Something went wrong.");
                //alert(data.message); <-- USE THIS IF YOU WANT TO DISPLAY THE MESSAGE FROM THE CONTROLLER
            }
        })
        .catch(error => console.error('Error:', error));
}

/*CONTACT MODAL*/
function openModalDriverContact(name, email, contact) {
    $('#modalUserName').text(name);
    $('#modalUserEmail').text(email);
    $('#modalUserContact').text(contact);

    $('#contactModal').modal('show');
}

function closeModalDriverContact() {
    $('#modalUserName').text('');
    $('#modalUserEmail').text('');
    $('#modalUserContact').text('');
    $('#contactModal').modal('hide');
}



/*DRIVER PICTURE MODAL*/
function openModalDriverPicture(driverPicture) {
    $('#driversPictureModal img').attr('src', driverPicture);

    $('#driversPictureModal').modal('show');
}

function closeModalDriverPicture() {
    $('#driversPictureModal img').attr('src', '');
    $('#driversPictureModal').modal('hide');
}



/*VIEW DRIVER'S LICENSE MODAL*/
function openModalViewDriverLicense(front,back) {
    $('#driversLicenseFrontImage').attr('src', front);
    $('#driversLicenseBackImage').attr('src', back);

    $('#driversLicenseModal').modal('show');
}

function closeModalViewDriverLicense() {
    $('#driversLicenseFrontImage').attr('src', '');
    $('#driversLicenseBackImage').attr('src', '');
    $('#driversLicenseModal').modal('hide');
}
