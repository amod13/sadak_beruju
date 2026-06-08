var tte = $.noConflict();

tte(function () {

    // ------------------ HELPER FUNCTIONS ------------------
    function hideAllDivs() {
        // Hide all DIVs
        tte("#Div_Ministry, #Div_Bivag, #Div_Nirdesha, #Div_Office, #Div_Aayog, #Div_Local").hide();

        // Reset dropdowns inside hidden DIVs
        tte("#dll_ministry, #dll_bivag, #dll_nirdesh, #dll_office, #dll_aayog, #dll_local").val("0").trigger("change");
    }

    function populateDropdown(dropdownId, data, defaultText = "--छान्नुहोस--") {
        let html = `<option value='0'>${defaultText}</option>`;
        tte.each(data, function (i, item) {
            html += `<option value='${item.Value}'>${item.Text}</option>`;
        });
        tte(`#${dropdownId}`).html(html);
    }

    // ------------------ INITIAL STATE ------------------
    hideAllDivs();

    // ------------------ OFFICE TYPE CHANGE ------------------
    tte("#dll_OfficeType").change(function () {
        let type = parseInt(tte(this).val());

        hideAllDivs(); // hide all and reset

        if (type === 2)     tte("#Div_Ministry").show();
        else if (type === 3) tte("#Div_Ministry, #Div_Bivag").show();
        else if (type === 4) tte("#Div_Ministry, #Div_Bivag, #Div_Nirdesha").show();
        else if (type === 5) tte("#Div_Ministry, #Div_Bivag, #Div_Nirdesha, #Div_Office").show();
        else if (type === 6) tte("#Div_Local").show();
    });

    tte("#dll_OfficeType").trigger("change"); // apply initial state

    // ------------------ CASCADING DROPDOWNS ------------------
    window.LoadBivag = function (url) {
        let ministryId = tte("#dll_ministry").val();
        if (parseInt(ministryId) > 0) {
            tte.ajax({
                url: url,
                type: "POST",
                data: { id: ministryId },
                dataType: "json",
                success: function (data) {
                    populateDropdown("dll_bivag", data);
                    populateDropdown("dll_nirdesh", []); // clear Nirdesh
                    populateDropdown("dll_office", []);  // clear Office
                },
                error: function () { alert("Error loading Bivag"); }
            });
        } else {
            populateDropdown("dll_bivag", []);
            populateDropdown("dll_nirdesh", []);
            populateDropdown("dll_office", []);
        }
    };

    window.LoadNirdesh = function (url) {
        let bivagId = tte("#dll_bivag").val();
        if (parseInt(bivagId) > 0) {
            tte.ajax({
                url: url,
                type: "POST",
                data: { id: bivagId },
                dataType: "json",
                success: function (data) {
                    populateDropdown("dll_nirdesh", data);
                    populateDropdown("dll_office", []);
                },
                error: function () { alert("Error loading Nirdeshanalaya"); }
            });
        } else {
            populateDropdown("dll_nirdesh", []);
            populateDropdown("dll_office", []);
        }
    };

    window.LoadOffice = function (url) {
        let nirdeshId = tte("#dll_nirdesh").val();
        if (parseInt(nirdeshId) > 0) {
            tte.ajax({
                url: url,
                type: "POST",
                data: { id: nirdeshId },
                dataType: "json",
                success: function (data) {
                    populateDropdown("dll_office", data);
                },
                error: function () { alert("Error loading Office"); }
            });
        } else {
            populateDropdown("dll_office", []);
        }
    };

    // ------------------ DROPDOWN CHANGE EVENTS ------------------
    tte("#dll_ministry").change(function () {
        if (typeof url_bivag !== "undefined") LoadBivag(url_bivag);
    });

    tte("#dll_bivag").change(function () {
        if (typeof url_nirdesh !== "undefined") LoadNirdesh(url_nirdesh);
    });

    tte("#dll_nirdesh").change(function () {
        if (typeof url_office !== "undefined") LoadOffice(url_office);
    });

});
