
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

function previewImages() {
    const previewContainer = document.getElementById('imagePreviewContainer');
    const files = document.getElementById('carImages').files;

    if (files.length > 0) {
        previewContainer.classList.remove('d-none'); // Show the preview container by removing 'd-none'
        previewContainer.style.display = 'flex'; // Show the preview container
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
                        updateFileInput(uploadedImages);
                    }
                    if (uploadedImages.length === 0) {
                        previewContainer.classList.add('d-none'); // Hide the preview container if no images left
                    }
                };

                imgContainer.appendChild(img);
                imgContainer.appendChild(closeButton);
                previewContainer.appendChild(imgContainer);
            }
            reader.readAsDataURL(files[i]);
            updateFileInput(uploadedImages);

        }
    }
}

function updateFileInput(files) {
    const dt = new DataTransfer();
    files.forEach(file => dt.items.add(file));
    document.getElementById('carImages').files = dt.files;
}

function openCarDetails(carId) {
    fetch(`/Admin/GetCarDetails?carId=${carId}`)
        .then(response => response.json())
        .then(data => {
            if (data.success) {
/*                document.getElementById('detailsContainer').innerHTML = data.data;*/
                document.getElementById('contentContainer').classList.add('hidden-important');
                document.getElementById('search-filter-div').classList.add('hidden-important');
                document.getElementById('add-item-div').classList.add('hidden-important');

                document.getElementById('detailsContainer').classList.remove('hidden-important');
                document.getElementById('detailsContainer').classList.add('visible');
            } else {
                reloadActivePartialView(data.message);
            }
        })
        .catch(error => console.error('Error:', error));
}

function openCarDetails(carId) {
    fetch(`/Admin/GetCarDetails?carId=${carId}`)
        .then(response => response.text())
        .then(data => {
            // Check if the car was not found
            if (data.includes('"success":false')) {
                reloadActivePartialView(data.message);
                return;
            }

            // Insert the returned HTML into the detailsContainer element
            //document.getElementById('detailsContainer').innerHTML = data;

            // Hide the contentContainer and show the detailsContainer


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