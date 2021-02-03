function openTab(evt, tabName) {
    // variables here
    var i, tabcontent, tablinks;

    //hide all class "tabcontent"
    tabcontent = document.getElementsByClassName("tabcontent");
  for (i = 0; i < tabcontent.length; i++) {
    tabcontent[i].style.display = "none";
  }

//remove active from tablinks
    tablinks = document.getElementsByClassName("tablinks");
  for (i = 0; i < tablinks.length; i++) {
    tablinks[i].className = tablinks[i].className.replace(" active", "");
  }

//show the currently clicked tab with 'active'
  document.getElementById(tabName).style.display = "block";
  evt.currentTarget.className += " active";
}
