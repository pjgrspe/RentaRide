/* ---------------------------------------------------
    DRIVERS TAB SCRIPT
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
                reloadActivePartialView("Driver successfully added.");
            } else {
                reloadActivePartialView("Something went wrong.");
            }
            closeModalAddDriver();
        })
        .catch(error => console.error('Error:', error));
}

/*EDIT DRIVER MODAL*/
function openModalEditDriver(id, firstname, middlename, lastname, email, contact, status) {
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
                reloadActivePartialView("Driver successfully edited.");
            } else {
                //FAILED
                reloadActivePartialView("Something went wrong.");
                //alert(data.message); <-- USE THIS IF YOU WANT TO DISPLAY THE MESSAGE FROM THE CONTROLLER
            }
            closeModalEditDriver();
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
                reloadActivePartialView("Driver successfully deleted.");
            } else {
                //FAILED
                reloadActivePartialView("Something went wrong.");
                //alert(data.message); <-- USE THIS IF YOU WANT TO DISPLAY THE MESSAGE FROM THE CONTROLLER
            }
            closeModalDeleteDriver()
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
function openModalViewDriverLicense(front, back) {
    $('#driversLicenseFrontImage').attr('src', front);
    $('#driversLicenseBackImage').attr('src', back);

    $('#driversLicenseModal').modal('show');
}

function closeModalViewDriverLicense() {
    $('#driversLicenseFrontImage').attr('src', '');
    $('#driversLicenseBackImage').attr('src', '');
    $('#driversLicenseModal').modal('hide');
}