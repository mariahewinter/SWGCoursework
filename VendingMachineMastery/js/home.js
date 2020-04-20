$(document).ready(function () {

loadItems();

});

// LOAD ALL ITEMS -------------------------------------------------------------
function loadItems() {
var itemsDisplay = $('#itemsDisplay');

  $.ajax ({
    type: 'GET',
      url: 'http://localhost:56854/' + "items/all",
    success: function(itemArray) {
      $.each(itemArray, function(index, item) {
        var itemId = item.id;
        var itemName = item.name;
        var itemQuantity = item.quantity
        var itemPrice = new Intl.NumberFormat('en-US', {
                style: 'currency',
                currency: 'USD',
                minimumFractionDigits: 2,
            }).format(item.price);


        var itemBox =  '<a onclick="updateItemIdDisplay(' + itemId + ');">'
            itemBox += '<div class="itemBox col-md-4"">'
            itemBox += '<div class="item-Id">'+ itemId +'</div><br><br>';
            itemBox += '<div class="itemContents"><div class="item-name">' + itemName +'</div><br><br>';
            itemBox += '<div class="item-price" id="'+ itemId +'">' + itemPrice +'</div><br><br>';
            itemBox += '<div class="item-quantity">Quantity Left: ' + itemQuantity + '</div><br><br>';
            itemBox += '</div>';
            itemBox += '</div>';
            itemBox += '</a>';

            itemsDisplay.append(itemBox);

      });
    },
    error: function() {

    }
  });
}
// ADD M O N E Y --------------------------------------------------------------
function addDollar() { //------------------------------------------ D O L L A R
  // check if the input fields need to be cleared after a successful Purchase
  if($('#display-messages').val() === "Thank you!!!")
  {
    $('#display-messages').val("");
    $('#change-return-display').val("");
  }
  // remove the dollar sign, otherwise you get NaN and that's bs
var dollars = ($('#money-added').val()).substring(1);

  var dollars = Number(dollars);
      dollars++;
      dollars = new Intl.NumberFormat('en-US', {
          style: 'currency',
          currency: 'USD',
          minimumFractionDigits: 2,
      }).format(dollars);


  $('#money-added').val(dollars);
}

function addQuarter() { //--------------------------------------- Q U A R T E R
  // check if the input fields need to be cleared after a successful Purchase
  if($('#display-messages').val() === "Thank you!!!")
  {
    $('#display-messages').val("");
    $('#change-return-display').val("");
  }
  // remove the dollar sign, otherwise you get NaN and that's bs
var dollars = ($('#money-added').val()).substring(1);

  var dollars = Number(dollars);
      dollars += Number(.25);
      dollars = new Intl.NumberFormat('en-US', {
          style: 'currency',
          currency: 'USD',
          minimumFractionDigits: 2,
      }).format(dollars);


  $('#money-added').val(dollars);
}

function addDime() { //------------------------------------------------ D I M E
  // check if the input fields need to be cleared after a successful Purchase
  if($('#display-messages').val() === "Thank you!!!")
  {
    $('#display-messages').val("");
    $('#change-return-display').val("");
  }
  // remove the dollar sign, otherwise you get NaN and that's bs
var dollars = ($('#money-added').val()).substring(1);

  var dollars = Number(dollars);
      dollars += Number(.10);
      dollars = new Intl.NumberFormat('en-US', {
          style: 'currency',
          currency: 'USD',
          minimumFractionDigits: 2,
      }).format(dollars);


  $('#money-added').val(dollars);
}

function addnickel() { //--------------------------------------------- N I C K E L
  // check if the input fields need to be cleared after a successful Purchase
  if($('#display-messages').val() === "Thank you!!!")
  {
    $('#display-messages').val("");
    $('#change-return-display').val("");
  }
  // remove the dollar sign, otherwise you get NaN and that's bs
var dollars = ($('#money-added').val()).substring(1);

  var dollars = Number(dollars);
      dollars += Number(.05);
      dollars = new Intl.NumberFormat('en-US', {
          style: 'currency',
          currency: 'USD',
          minimumFractionDigits: 2,
      }).format(dollars);


  $('#money-added').val(dollars);
}

// SELECTING AN ITEM ----------------------------------------------------------
function updateItemIdDisplay(itemId) {
  if($('#display-messages').val() === "Thank you!!!")
  {
    $('#money-added').val("$0.00");
    $('#display-messages').val("");
    $('#change-return-display').val("");
  }
  $('#item-id-display').val(itemId);

}

// VEND ITEM ------------------------------------------------------------------
function makePurchase()  {
var itemId = $('#item-id-display').val();
var moneyAdded = $('#money-added').val().substring(1);

if (itemId == "")
{
  $('#display-messages').val("Please make a selection");
}
else{
      $('#display-messages').val("");

  $.ajax ({
    type: 'POST',
      url: 'http://localhost:56854/money/' + moneyAdded + '/item/' + itemId,
    success: function(coinage) {
          var change = Number(coinage.quarters)
              change += Number(coinage.dimes)
              change += Number(coinage.nickels)
              change += Number(coinage.pennies);

          if(change === 0)
          {
            $('#display-messages').val("Thank you!!!");
          }
          else {
            $('#display-messages').val("Thank you!!!");
            $('#change-return-display').val(coinage.quarters + " Quarters, " + coinage.dimes + " Dimes, " + coinage.nickels + " Nickels, " + coinage.pennies + " Pennies");
          }
          clearItems();
          $('#money-added').val("$0.00");
          $('#item-id-display').val("");
          loadItems();
          },
          error: function(xhr,status,error) {

            var json = JSON.parse(xhr.responseText);
            var message = json.Message;

            $('#display-messages').val(message);
          }
  });

}


}

function changeReturn() {

  var remainingCents = $('#money-added').val().substring(1) * 100;

    if(remainingCents == 0){
      zeroChangeDisplay();
    }
    zeroTotalIn();

    if($('#change-return-display').val() == "")
    {
      zeroMessages();
    }

    var quarter = 0;
    var dime = 0;
    var nickel = 0;
    var penny = 0;

  var change = "";

  while(remainingCents >= 25) {
    remainingCents -= 25;
    quarter++;
  }
  if(quarter == 1){
    change += "1 quarter, ";
  }
  if(quarter > 1){
    change += quarter + " quarters, ";
  }
  while (remainingCents >= 10) {
    remainingCents -= 10;
    dime++;
  }
  if(dime === 1){
    change += " 1 dime, ";
  }
  if(dime > 1){
    change += dime + " dimes, "
  }
  while (remainingCents >= 05) {
    remainingCents -= 05;
    nickel++;
  }
  if(nickel === 1){
    change += " 1 nickel, ";
  }
  if(nickel > 1){
    change += nickel + " nickels, "
  }
  while (remainingCents > 0) {
    remainingCents -= 01;
    penny++;
  }
  if(penny === 1){
    change += " 1 penny";
  }
  if(penny > 1){
    change += penny + " pennies"
  }

  if(change.charAt(change.length - 2) == ','){
    change = change.substring(0, change.length - 2);
  }

$('#change-return-display').val(change);

    }

function zeroTotalIn() {
  $('#money-added').val("$0.00");
}

function zeroChangeDisplay() {
  $('#change-return-display').val("");
}

function zeroMessages() {
  $('#display-messages').val("");
}

function clearItems() {
  $('#itemsDisplay').empty();
}
