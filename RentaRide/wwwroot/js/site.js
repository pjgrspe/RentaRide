document.addEventListener("DOMContentLoaded", function () {
    var lazyloadImages = document.querySelectorAll("img.lazyload");
    if ("IntersectionObserver" in window) {
        let lazyloadObserver = new IntersectionObserver(function (entries, observer) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    let img = entry.target;
                    img.src = img.dataset.src;
                    img.classList.remove("lazyload");
                    lazyloadObserver.unobserve(img);
                }
            });
        });

        lazyloadImages.forEach(function (img) {
            lazyloadObserver.observe(img);
        });
    } else {
        // Fallback for older browsers
        let lazyloadThrottleTimeout;
        function lazyload() {
            if (lazyloadThrottleTimeout) {
                clearTimeout(lazyloadThrottleTimeout);
            }
            lazyloadThrottleTimeout = setTimeout(function () {
                let scrollTop = window.pageYOffset;
                lazyloadImages.forEach(function (img) {
                    if (img.offsetTop < (window.innerHeight + scrollTop)) {
                        img.src = img.dataset.src;
                        img.classList.remove('lazyload');
                    }
                });
                if (lazyloadImages.length == 0) {
                    document.removeEventListener("scroll", lazyload);
                    window.removeEventListener("resize", lazyload);
                    window.removeEventListener("orientationChange", lazyload);
                }
            }, 20);
        }

        document.addEventListener("scroll", lazyload);
        window.addEventListener("resize", lazyload);
        window.addEventListener("orientationChange", lazyload);
    }
});

/* ---------------------------------------------------
    FLATPICKR SCRIPT
----------------------------------------------------- */
document.addEventListener('DOMContentLoaded', function () {
    function initializeFlatpickr(selector, options) {
        flatpickr(selector, options);
    }

    // Calculate the date 18 years ago from today
    const today = new Date();
    const maxDate18 = new Date(today.getFullYear() - 18, today.getMonth(), today.getDate()).toISOString().split('T')[0];

    // Initialize Flatpickr for the "Date of Birth" field with age restriction
    initializeFlatpickr(".flatpickr-register", {
        dateFormat: "Y-m-d",
        maxDate: maxDate18,
        altInput: true,
        altFormat: "F j, Y",
        allowInput: true,
    });
});

/* ---------------------------------------------------
    LOADER SCRIPT
----------------------------------------------------- */
function loadMainContent() {
    // Show the spinner initially
    document.getElementById('loadingSpinner').style.display = 'block';

    // Hide the spinner and show the content after a delay
    $(document).ready(function () {
        setTimeout(function () {
            $('#loadingSpinner').hide();
            $('.load-content').show(); // Show the content
        }, 500); // Adjust the delay (in milliseconds) as needed
    });
}

// Example of calling the function when the DOM is fully loaded
document.addEventListener("DOMContentLoaded", function () {
    loadMainContent();
});

