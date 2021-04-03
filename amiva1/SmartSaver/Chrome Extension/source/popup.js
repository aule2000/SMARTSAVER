document.addEventListener('DOMContentLoaded', documentEvents  , false);

function documentEvents() {    
  document.getElementById('saveButton').addEventListener('click', 
    function() { 
        userId = document.getElementById('userId');
        console.debug("1");
        var regexGuid = new RegExp('^([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})$', 'i');

        if(!regexGuid.test(userId.value)){
            document.getElementById('messageLabel').innerHTML = 'UserId is not of the type Guid';
            return;
        }

        storeConfig(userId);
  });

  document.getElementById('spendButton').addEventListener('click', 
    function() { 
        var balances = document.querySelector('input[name="balance"]:checked').value;
        var amount = document.getElementById('amount').value;
        var category = document.getElementById('category').value;
        var description = document.getElementById('description').value;

        chrome.storage.sync.get(['UserId'], function(items) {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function() {
            if (xhr.readyState == XMLHttpRequest.DONE && xhr.status == 200) {
                document.getElementById('messageLabel').innerHTML = 'Spent';
            } else{
                document.getElementById('messageLabel').innerHTML = 'Bad request';
            }
        }
        xhr.open('PUT', 'https://localhost:44301/transactions/' + items.UserId + '/' + balances + '/' + category + '/' + description +'/'+  amount, true);
        xhr.send();
        });
  });

  populateInputFields();
}

function storeConfig(userId, organizationId, functionUrl) { 
    chrome.storage.sync.set({
        'UserId': userId.value
    }, function() {
        document.getElementById('messageLabel').innerHTML = 'Settings saved';
    });  
}

function populateInputFields(){
    chrome.storage.sync.get(['UserId'], function(items) {
        document.getElementById('userId').value = items.UserId;
    });
}
