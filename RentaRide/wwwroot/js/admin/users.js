/* ---------------------------------------------------
    USER TAB SCRIPTS
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
                    reloadActivePartialView("User approved.");
                }
                else if (isVerified == false) {
                    //JASON INSERT YOUR JS CODE HERE
                    reloadActivePartialView("User denied.");
                }
                else {
                    //JASON INSERT YOUR JS CODE HERE
                    reloadActivePartialView("User set to Pending.");
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
