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
