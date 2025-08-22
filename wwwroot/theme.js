window.themeInterop = {
    init: function () {
        console.log("Theme JS initialized");

        // Ensure tooltips work
        $(".tooltips").tooltip();

        // Toggle Theme Panel
        $(".theme-panel .toggler").click(function () {
            $(".theme-panel").toggleClass("open");
        });

        $(".theme-panel .toggler-close").click(function () {
            $(".theme-panel").removeClass("open");
        });

        // Change Theme Color
        $(".theme-colors li").click(function () {
            let newTheme = $(this).attr("data-style");
            document.documentElement.setAttribute("data-theme", newTheme);
            console.log("Theme changed to: " + newTheme);
        });
    }
};
