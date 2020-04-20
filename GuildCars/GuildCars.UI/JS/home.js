$(document).ready(function () {
    alert("Ready!");
});

function clearSearchResultsDisplayNew() {
    $('#searchResultsDisplayNew').empty();
}

function clearSearchResultsDisplayUsed() {
    $('#searchResultsDisplayUsed').empty();
}

function clearSearchResultsDisplayAdmin() {
    $('#searchResultsDisplayAdmin').empty();
}

function clearSearchResultsDisplaySales() {
    $('#searchResultsDisplaySales').empty();
}


function detailsView(VinNumber) {
    window.location.href = "https://localhost:44378/Inventory/Details?vinNumber=" + VinNumber;
}

function contactUsView(VinNumber) {
    window.location.href = "https://localhost:44378/Home/Contact?vinNumber=" + VinNumber;
}

function editVehicleView(VinNumber) {
    window.location.href = "https://localhost:44378/Admin/EditVehicle?vinNumber=" + VinNumber;
}

function purchaseVehicleView(VinNumber) {
    window.location.href = "https://localhost:44378/Sales/Purchase?vinNumber=" + VinNumber;
}

function editUserView(UserID) {
    window.location.href = "https://localhost:44378/Admin/EditUser?userID=" + UserID;
}

function deleteSpecialAlert(specialID) {
    if (confirm('Are you sure you want to delete this Special?')) {
        deleteSpecial(specialID)
    }
}

function deleteSpecial(specialID) {
    $.ajax({
        type: 'DELETE',
        url: 'https://localhost:44378/api/Admin/Specials/SpecialID/' + specialID,
        success: function (status) {
            location.reload();
        }
    })
}

function deleteVehicleAlert(vinNumber) {
    if (confirm('Are you sure you want to delete this Vehicle?')) {
        deleteVehicle(vinNumber)
    }
}

function deleteVehicle(vinNumber) {
    $.ajax({
        type: 'DELETE',
        url: 'https://localhost:44378/api/Admin/Vehicles/VinNumber/' + vinNumber,
        success: function (status) {
            
        }
    })
}

$("#addVehicleMake").change(function () {
    var makeID = $('#addVehicleMake').val();
    alert("The makeID is now: " + makeID);

    var addVehicleModel = $('#addVehicleModel');

    addVehicleModel.empty();
    addVehicleModel.append('<option value="" selected disabled>Model</option>');

    $.ajax({
        type: 'GET',
        url: 'https://localhost:44378/api/Admin/AddVehicle/MakeID/' + makeID,
        success: function (modelArray) {

            $.each(modelArray, function (index, model) {
                var ModelID = model.ModelID;
                var ModelName = model.ModelName;

                var addOption = '<option value="' + ModelID + '">' + ModelName + '</option>'

                addVehicleModel.append(addOption);
            });

        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service.  Please try again later.'));
        }
    });

}); // ----------------------------------------- ADMIN/ADDVEHICLE

$("#editVehicleMake").change(function () {
    var makeID = $('#editVehicleMake').val();
    alert("The makeID is now: " + makeID);

    var editVehicleModel = $('#editVehicleModel');

    editVehicleModel.empty();
    editVehicleModel.append('<option value="" selected disabled>Model</option>');

    $.ajax({
        type: 'GET',
        url: 'https://localhost:44378/api/Admin/EditVehicle/MakeID/' + makeID,
        success: function (modelArray) {

            $.each(modelArray, function (index, model) {
                var ModelID = model.ModelID;
                var ModelName = model.ModelName;

                var addOption = '<option value="' + ModelID + '">' + ModelName + '</option>'

                editVehicleModel.append(addOption);
            });

        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service.  Please try again later.'));
        }
    });

}); // ----------------------------------------- ADMIN/EDITVEHICLE


$('#searchButtonNew').click(function (event) {
    // will need to clear the table in the event of them doing back to back searches
    clearSearchResultsDisplayNew(); 

    var searchResultsDisplayNew = $('#searchResultsDisplayNew');
    var mileageNew = -2;
    var searchTermNew = "chipmunk";

    if ($('#searchTermNew').val() != "") {
        searchTermNew = $('#searchTermNew').val();
    }

    $.ajax({
        type: 'GET',
        url: 'https://localhost:44378/api/Inventory/New/SearchTerm/' + searchTermNew + '/PriceMin/' + $('#priceMinNew').val() + '/PriceMax/' + $('#priceMaxNew').val() + '/YearMin/' + $('#yearMinNew').val() + '/YearMax/' + $('#yearMaxNew').val() + '/Mileage/' + mileageNew,
        success: function (vehicleArray) {
            $.each(vehicleArray, function (index, vehicle) {
                var VinNumber = vehicle.Vehicle.VinNumber;
                var ModelName = vehicle.Model.ModelName;
                var MakeName = vehicle.Make.MakeName;
                var BodyStyleName = vehicle.BodyStyle.BodyStyleName;
                var TransmissionName = vehicle.Transmission.TransmissionName;
                var ExteriorColor = vehicle.ExteriorColor.ColorName;
                var InteriorColor = vehicle.InteriorColor.ColorName;
                var Year = vehicle.Vehicle.Year;
                var Mileage = vehicle.Vehicle.Mileage;
                var MSRP = vehicle.Vehicle.MSRP;
                var SalePrice = vehicle.Vehicle.SalePrice;
                var Description = vehicle.Vehicle.Description;
                var Picture = vehicle.Vehicle.Picture;
                var IsFeatured = vehicle.Vehicle.IsFeatured;
                var IsPurchased = vehicle.Vehicle.IsPurchased;



                var vehicleDisplay = '<div class="row">'
                vehicleDisplay += '<div class="col-md-3">'
                vehicleDisplay += '<div class="well">' + Year + ' ' + MakeName + ' ' + ModelName + '<img src="/Images/' + Picture + '"></img></div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-9">'
                vehicleDisplay += '<div class="row">'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Body Style: </strong>' + BodyStyleName + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Interior: </strong>' + InteriorColor + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Sale Price: </strong>$' + SalePrice + '.00</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="row">'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Trans: </strong>' + TransmissionName + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Mileage: </strong> New </div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>MSRP: </strong>$' + MSRP + '.00</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Color: </strong>' + ExteriorColor + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>VIN #: </strong>' + VinNumber + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well">'
                vehicleDisplay += '<button type="button" class="btn-block btn-primary" onclick="detailsView('+"'"+VinNumber+"'"+')">Details</button>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'


                searchResultsDisplayNew.append(vehicleDisplay);

            });
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service.  Please try again later.'));
        }
    });
}) // ----------------------------------------- INVENTORY/NEW

$('#searchButtonUsed').click(function (event) {
    // will need to clear the table in the event of them doing back to back searches
    clearSearchResultsDisplayUsed();

    var searchResultsDisplayUsed = $('#searchResultsDisplayUsed');
    var mileageUsed = -1;
    var searchTermUsed = "chipmunk";

    if ($('#searchTermUsed').val() != "") {
        searchTermUsed = $('#searchTermUsed').val();
    }

    $.ajax({
        type: 'GET',
        url: 'https://localhost:44378/api/Inventory/Used/SearchTerm/' + searchTermUsed + '/PriceMin/' + $('#priceMinUsed').val() + '/PriceMax/' + $('#priceMaxUsed').val() + '/YearMin/' + $('#yearMinUsed').val() + '/YearMax/' + $('#yearMaxUsed').val() + '/Mileage/' + mileageUsed,
        success: function (vehicleArray) {
            $.each(vehicleArray, function (index, vehicle) {
                var VinNumber = vehicle.Vehicle.VinNumber;
                var ModelName = vehicle.Model.ModelName;
                var MakeName = vehicle.Make.MakeName;
                var BodyStyleName = vehicle.BodyStyle.BodyStyleName;
                var TransmissionName = vehicle.Transmission.TransmissionName;
                var ExteriorColor = vehicle.ExteriorColor.ColorName;
                var InteriorColor = vehicle.InteriorColor.ColorName;
                var Year = vehicle.Vehicle.Year;
                var Mileage = vehicle.Vehicle.Mileage;
                var MSRP = vehicle.Vehicle.MSRP;
                var SalePrice = vehicle.Vehicle.SalePrice;
                var Description = vehicle.Vehicle.Description;
                var Picture = vehicle.Vehicle.Picture;
                var IsFeatured = vehicle.Vehicle.IsFeatured;
                var IsPurchased = vehicle.Vehicle.IsPurchased;



                var vehicleDisplay = '<div class="row">'
                vehicleDisplay += '<div class="col-md-3">'
                vehicleDisplay += '<div class="well">' + Year + ' ' + MakeName + ' ' + ModelName + '<img src="/Images/' + Picture + '"></img></div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-9">'
                vehicleDisplay += '<div class="row">'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Body Style: </strong>' + BodyStyleName + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Interior: </strong>' + InteriorColor + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Sale Price: </strong>$' + SalePrice + '.00</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="row">'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Trans: </strong>' + TransmissionName + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Mileage: </strong>' + Mileage + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>MSRP: </strong>$' + MSRP + '.00</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Color: </strong>' + ExteriorColor + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>VIN #: </strong>' + VinNumber + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<button type="button" class="btn-block btn-primary" onclick="detailsView(' + "'" + VinNumber + "'" + ')">Details</button>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'


                searchResultsDisplayUsed.append(vehicleDisplay);

            });
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service.  Please try again later.'));
        }
    });
}) // ----------------------------------------- INVENTORY/USED

$('#searchButtonAdmin').click(function (event) {
    // will need to clear the table in the event of them doing back to back searches
    clearSearchResultsDisplayAdmin();

    var searchResultsDisplayAdmin = $('#searchResultsDisplayAdmin');
    var mileageAdmin = -3;
    var searchTermAdmin = "chipmunk";

    if ($('#searchTermAdmin').val() != "") {
        searchTermAdmin = $('#searchTermAdmin').val();
    }

    $.ajax({
        type: 'GET',
        url: 'https://localhost:44378/api/Admin/Vehicles/SearchTerm/' + searchTermAdmin + '/PriceMin/' + $('#priceMinAdmin').val() + '/PriceMax/' + $('#priceMaxAdmin').val() + '/YearMin/' + $('#yearMinAdmin').val() + '/YearMax/' + $('#yearMaxAdmin').val() + '/Mileage/' + mileageAdmin,
        success: function (vehicleArray) {
            $.each(vehicleArray, function (index, vehicle) {
                var VinNumber = vehicle.Vehicle.VinNumber;
                var ModelName = vehicle.Model.ModelName;
                var MakeName = vehicle.Make.MakeName;
                var BodyStyleName = vehicle.BodyStyle.BodyStyleName;
                var TransmissionName = vehicle.Transmission.TransmissionName;
                var ExteriorColor = vehicle.ExteriorColor.ColorName;
                var InteriorColor = vehicle.InteriorColor.ColorName;
                var Year = vehicle.Vehicle.Year;
                var Mileage = vehicle.Vehicle.Mileage;
                var MSRP = vehicle.Vehicle.MSRP;
                var SalePrice = vehicle.Vehicle.SalePrice;
                var Description = vehicle.Vehicle.Description;
                var Picture = vehicle.Vehicle.Picture;
                var IsFeatured = vehicle.Vehicle.IsFeatured;
                var IsPurchased = vehicle.Vehicle.IsPurchased;



                var vehicleDisplay = '<div class="row">'
                vehicleDisplay += '<div class="col-md-3">'
                vehicleDisplay += '<div class="well">' + Year + ' ' + MakeName + ' ' + ModelName + '<img src="/Images/' + Picture + '"></img></div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-9">'
                vehicleDisplay += '<div class="row">'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Body Style: </strong>' + BodyStyleName + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Interior: </strong>' + InteriorColor + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Sale Price: </strong>$' + SalePrice + '.00</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="row">'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Trans: </strong>' + TransmissionName + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Mileage: </strong>' + Mileage + '</div > '
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>MSRP: </strong>$' + MSRP + '.00</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Color: </strong>' + ExteriorColor + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>VIN #: </strong>' + VinNumber + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<button type="button" class="btn-block btn-primary" onclick="editVehicleView(' + "'" + VinNumber + "'" + ')">Edit</button>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'


                searchResultsDisplayAdmin.append(vehicleDisplay);

            });
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service.  Please try again later.'));
        }
    });
}) // ----------------------------------------- ADMIN/VEHICLES


$('#searchButtonSales').click(function (event) {  // will need to clear the table in the event of them doing back to back searches
    clearSearchResultsDisplaySales();

    var searchResultsDisplaySales = $('#searchResultsDisplaySales');
    var mileageSales = -3;
    var searchTermSales = "chipmunk";

    if ($('#searchTermSales').val() != "") {
        searchTermSales = $('#searchTermSales').val();
    }

    $.ajax({
        type: 'GET',
        url: 'https://localhost:44378/api/Sales/Index/SearchTerm/' + searchTermSales + '/PriceMin/' + $('#priceMinSales').val() + '/PriceMax/' + $('#priceMaxSales').val() + '/YearMin/' + $('#yearMinSales').val() + '/YearMax/' + $('#yearMaxSales').val() + '/Mileage/' + mileageSales,
        success: function (vehicleArray) {
            $.each(vehicleArray, function (index, vehicle) {
                var VinNumber = vehicle.Vehicle.VinNumber;
                var ModelName = vehicle.Model.ModelName;
                var MakeName = vehicle.Make.MakeName;
                var BodyStyleName = vehicle.BodyStyle.BodyStyleName;
                var TransmissionName = vehicle.Transmission.TransmissionName;
                var ExteriorColor = vehicle.ExteriorColor.ColorName;
                var InteriorColor = vehicle.InteriorColor.ColorName;
                var Year = vehicle.Vehicle.Year;
                var Mileage = vehicle.Vehicle.Mileage;
                var MSRP = vehicle.Vehicle.MSRP;
                var SalePrice = vehicle.Vehicle.SalePrice;
                var Description = vehicle.Vehicle.Description;
                var Picture = vehicle.Vehicle.Picture;
                var IsFeatured = vehicle.Vehicle.IsFeatured;
                var IsPurchased = vehicle.Vehicle.IsPurchased;



                var vehicleDisplay = '<div class="row">'
                vehicleDisplay += '<div class="col-md-3">'
                vehicleDisplay += '<div class="well">' + Year + ' ' + MakeName + ' ' + ModelName + '<img src="/Images/' + Picture + '"></img></div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-9">'
                vehicleDisplay += '<div class="row">'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Body Style: </strong>' + BodyStyleName + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Interior: </strong>' + InteriorColor + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Sale Price: </strong>$' + SalePrice + '.00</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="row">'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Trans: </strong>' + TransmissionName + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Mileage: </strong>' + Mileage + '</div > '
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>MSRP: </strong>$' + MSRP + '.00</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>Color: </strong>' + ExteriorColor + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<div class="well"><strong>VIN #: </strong>' + VinNumber + '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '<div class="col-md-4">'
                vehicleDisplay += '<button type="button" class="btn-block btn-primary" onclick="purchaseVehicleView(' + "'" + VinNumber + "'" + ')">Purchase</button>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'
                vehicleDisplay += '</div>'


                searchResultsDisplaySales.append(vehicleDisplay);

            });
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service.  Please try again later.'));
        }
    });
}) // ----------------------------------------- SALES/INDEX

