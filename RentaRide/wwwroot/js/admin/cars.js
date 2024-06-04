
/* ---------------------------------------------------
   CAR TAB SCRIPTS
----------------------------------------------------- */
/*ADD CAR MODAL*/
function openModalAddCar() {
    $('#addNewCarModal').modal('show');
}

function closeModalAddCar() {
    $('#addNewCarModal').modal('hide');
}

function addCar() {
    var formData = new FormData();
    const files = document.getElementById('carImages').files;
    var carTransBool = document.getElementById('carTrans').value;
    var carFuelBool = document.getElementById('carFuelType').value;

    for (let i = 0; i < files.length; i++) {
        formData.append('caraddImages', files[i]);
    }

    formData.append('caraddMake', $('#carMake').val());
    formData.append('caraddModel', $('#carModel').val());
    formData.append('caraddYear', $('#carYear').val());
    formData.append('caraddType', $('#carType').val());
    formData.append('caraddColor', $('#carColor').val());
    formData.append('caraddPlateNumber', $('#carLicenseNum').val());
    formData.append('caraddORDoc', $('#carOR')[0].files[0]);
    formData.append('caraddCRDoc', $('#carCR')[0].files[0]);
    formData.append('caraddTrans', carTransBool);
    formData.append('caraddFuelType', carFuelBool);
    formData.append('caraddSeats', $('#carSeats').val());
    formData.append('caraddMileage', $('#carMileage').val());
    formData.append('caraddLastChangeOilMileage', $('#carChangeOilDate').val());
    formData.append('caraddOilChangeInterval', $('#carChangeOilInterval').val());
    formData.append('caraddLastMaintenance', $('#carLastMaintenanceDate').val());

    

    fetch('/Admin/AddNewCar', {
        method: 'POST',
        body: formData,
        data: formData,
        processData: false,
        contentType: false
    })
        .then(response => response.text().then(text => text ? JSON.parse(text) : {}))
        .then(data => {
            if (data.success) {
                reloadActivePartialView("Car successfully added.");
            } else {
                reloadActivePartialView("Something went wrong.");
            }
            closeModalAddCar();
        })
        .catch(error => console.error('Error:', error));
    
}

let uploadedImages = [];

function previewImages(inputElementId, previewContainerId) {
    const previewContainer = document.getElementById(previewContainerId);
    const files = document.getElementById(inputElementId).files;

    if (files.length > 0) {
        previewContainer.classList.remove('d-none');
        previewContainer.style.display = 'flex';

        for (let i = 0; i < files.length; i++) {
            uploadedImages.push(files[i]);

            const reader = new FileReader();
            reader.onload = function (e) {
                const imgContainer = document.createElement('div');
                imgContainer.setAttribute('class', 'position-relative d-inline-block m-2');

                const img = document.createElement('img');
                img.setAttribute('src', e.target.result);
                img.setAttribute('class', 'img-thumbnail');
                img.setAttribute('style', 'width: 100px; height: 100px; object-fit: cover;');

                const closeButton = document.createElement('button');
                closeButton.setAttribute('type', 'button');
                closeButton.setAttribute('class', 'preview-close-btn');
                closeButton.innerHTML = '&times;';
                closeButton.onclick = function () {
                    previewContainer.removeChild(imgContainer);
                    const index = uploadedImages.indexOf(files[i]);
                    if (index > -1) {
                        uploadedImages.splice(index, 1);
                        updateFileInput(inputElementId, uploadedImages);
                    }
                    if (uploadedImages.length === 0) {
                        previewContainer.classList.add('d-none');
                    }
                };

                imgContainer.appendChild(img);
                imgContainer.appendChild(closeButton);
                previewContainer.appendChild(imgContainer);
            }
            reader.readAsDataURL(files[i]);
            updateFileInput(inputElementId, uploadedImages);
        }
    }
}

function updateFileInput(inputElementId, files) {
    const dt = new DataTransfer();
    files.forEach(file => dt.items.add(file));
    document.getElementById(inputElementId).files = dt.files;
}


function openModalAddLog(carID) {
    $('#logCarID').val(carID);
    $('#addNewLogModal').modal('show');
}

function closeModalAddLog() {
    $('#addNewLogModal').modal('hide');
}

function openORModal(ORPicture) {
    $('#carsORPictureModal').modal('show');
    $('#carsORPictureModal img').attr('src', ORPicture);
}

function openCRModal(CRPicture) {
    $('#carsCRPictureModal').modal('show');
    $('#carsCRPictureModal img').attr('src', CRPicture);
}

function openCarDetails(carId) {
    fetch(`/Admin/GetCarDetails?carId=${carId}&forDetailsPage=true`)
        .then(response => response.text())
        .then(data => {
            // Check if the car was not found
            if (data.includes('"success":false')) {
                reloadActivePartialView(data.message);
                return;
            }

            document.getElementById('detailsContainer').innerHTML = data;
            document.getElementById('contentContainer').classList.add('hidden-important');
            document.getElementById('search-filter-div').classList.add('hidden-important');
            document.getElementById('add-item-div').classList.add('hidden-important');

            document.getElementById('detailsContainer').classList.remove('hidden-important');
            document.getElementById('detailsContainer').classList.add('visible');
        })
        .catch(error => console.error('Error:', error));
}

function closeCarDetails() {
    document.getElementById('contentContainer').classList.remove('hidden-important');
    document.getElementById('search-filter-div').classList.remove('hidden-important');
    document.getElementById('add-item-div').classList.remove('hidden-important');

    document.getElementById('detailsContainer').classList.remove('visible');
    document.getElementById('detailsContainer').classList.add('hidden-important');
}

function openModalEditCarDetails(carId) {
    fetch(`/Admin/GetCarDetails?carId=${carId}&forDetailsPage=false`)
        .then(response => response.text())
        .then(data => {
            // Check if the car was not found
            if (data.includes('"success":false')) {
                reloadActivePartialView(data.message);
                return;
            }

            document.getElementById('modalContainer').innerHTML = data;
            $('#editcarModal').modal('show');


        })
        .catch(error => console.error('Error:', error));

}

function closeModalEditCarDetails() {
    $('#editcarModal').modal('hide');
}

function addLog() {
    var formData = new FormData();
    formData.append('addLogCarID', $('#logCarID').val());
    formData.append('addLogDetails', $('#logDetails').val());
    formData.append('addLogType', $('#logType').val());
    formData.append('addLogMileage', $('#logMileage').val());
    formData.append('addLogDate', $('#logDate').val());


    fetch('/Admin/AddNewLog', {
        method: 'POST',
        body: formData,
        data: formData,
        processData: false,
        contentType: false
    })
        .then(response => response.text().then(text => text ? JSON.parse(text) : {}))
        .then(data => {
            if (data.success) {
                reloadDetailsContainer("Log successfully added.");
            } else {
                reloadDetailsContainer(data.message);
            }
            closeModalAddLog();
        })
        .catch(error => console.error('Error:', error));

}



//DELETING CAR MODALS
function openModalDeleteCar() {
    $('#deleteCarModal').modal('show');
}
function closeModalDeleteCar() {
    $('#deleteCarModal').modal('hide');
}

//VIEW LOG DETAILS MODALS
function openModalLogDetails() {
    $('#logDetailsModal').modal('show');
}

function closeModalLogDetails() {
    $('#logDetailsModal').modal('hide');
}

//EDIT LOGS MODAL
function openModalEditLogDetails() {

}

function closeModalEditLogDetails() {

}

//DELETE LOGS MODAL
function openModalDeleteLogDetails() {
    $('#deleteLogModal').modal('show');
}

function closeModalDeleteLogDetails() {
    $('#deleteLogModal').modal('hide');
}




    