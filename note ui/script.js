const saveButton = document.querySelector('#btnSave');
const titleInput = document.querySelector('#title');
const descriptionInput = document.querySelector('#description');

const notesContainer = document.querySelector('#notes_container');


function clearForm(){
    titleInput.value = '';
    descriptionInput.value = '';
}


function addNote(title,description){

    const body = {
        title: title,
        description:description,
        isVisible:true
    };
    fetch('https://localhost:7060/api/Note',{
        method:'POST',
        body:JSON.stringify(body),
        headers:{
            "Content-Type": "application/json"
        }
    })
    .then(data=>data.json())
    .then(response => {
        clearForm(),
        getAllNotes()

    });
}

function displayNotes(notes){
    let allNote = '';
    notes.forEach(element => {

       const noteElement = `
                            <div class="note">
                                <h3>${element.title}</h3>
                                <p>${element.description}</p>
                            </div>
                            `;
                            allNote += noteElement;
        
    });
    notesContainer.innerHTML = allNote;

}
function getAllNotes(){
    fetch('https://localhost:7060/api/Note')
    .then(data=>data.json())
    .then(response => displayNotes(response));

}

getAllNotes();
saveButton.addEventListener('click',function(){

    addNote(titleInput.value,descriptionInput.value)
});

