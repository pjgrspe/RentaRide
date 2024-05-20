// Show the spinner initially
document.getElementById('loadingSpinner').style.display = 'block';

$(document).ready(function () {
    // Delay hiding the spinner for a more visible effect
    setTimeout(function () {
        $('#loadingSpinner').hide();
        $('.load-content').show(); // Show the content
    }, 1000); // Adjust the delay (in milliseconds) as needed
});

$(document).ready(function () {
    $("#sidebarCollapse").on("click", function () {
        $("#sidebar").toggleClass("active");
        $(this).toggleClass("active");
    });
});
