import { Component, OnInit } from '@angular/core';
import { EntryService } from '../../services/Entry.service';
import { NewEntry, Entry } from '../../models/entry.model'; 
import { CategoryService } from '../../services/category.service';
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common'; 
import { Category } from '../../models/category.model'; 

@Component({
  selector: 'app-entry-list',
  templateUrl: './entry-list.component.html', 
  styleUrls: ['./entry-list.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule] 
})
export class EntryListComponent implements OnInit {
  entries: Entry[] = [];
  filter: string = '';
  isFormVisible: boolean = false;
  isEditing: boolean = false;
  currentEntry: NewEntry = { id: '', title: '', content: '', category: '', user: '' };
  categories: Category[] = [];
  newCategoryName: string = ''; 
  isCategoryFormVisible: boolean = false; 
  currentPage: number = 1; 
  totalEntries: number = 50; 
  pageSize: number = 2; 
  
  constructor(private entryService: EntryService, private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.loadEntries();
    this.loadCategories();
  }

  loadEntries() {
    this.entryService.getEntries(this.currentPage, this.pageSize, this.filter).subscribe({
        next: (response) => {
            this.entries = response.entries;
            this.totalEntries = response.total;
        },
        error: (error) => {
            console.error('Error al cargar las entradas', error);
            console.log('Error Status:', error.status);
            console.log('Error StatusText:', error.statusText);
            console.log('Error Response:', error.error);
            this.entries = [];
        }
    });
}




  loadCategories() {
    this.categoryService.getAllCategories().subscribe({
      next: (response) => {
        this.categories = response;
      },
      error: (error) => {
        console.error('Error al cargar las categorías', error);
      }
    });
  }

  startCreating() {
    this.isFormVisible = true;
    this.isEditing = false;
    this.currentEntry = { id: '', title: '', content: '', user: '', category: '' }; 
  }

  convertEntryToNewEntry(entry: Entry): NewEntry {
    return {
      id: entry.id,
      title: entry.title,
      content: entry.content,
      category: entry.categoryName, 
      user: localStorage.getItem('userId') || '',
    };
  }
  convertNewEntryToEntry(newEntry: NewEntry, publicationDate: Date, userName: string): Entry {
    return {
        id: newEntry.id,
        title: newEntry.title,
        content: newEntry.content,
        publicationDate: publicationDate,
        userName: userName, 
        categoryName: newEntry.category 
    };
}


  editEntry(entry: Entry) {
    this.currentEntry = this.convertEntryToNewEntry(entry); 
    this.isEditing = true;
    this.isFormVisible = true; 
  }
  

  cancelForm() {
    this.isFormVisible = false;
    this.currentEntry = { id: '', title: '', content: '', user: '', category: '' };
  }

  onSubmit() {
    const userName = localStorage.getItem('userName') || ''; 

    if (this.isEditing) {
    const selectedCategory = this.categories.find(cat => cat.description === this.currentEntry.category);
    console.log(this.categories);
    if (selectedCategory) {
        this.currentEntry.category = selectedCategory.id; 
    } else {
        console.error('Categoría no encontrada');
        return;
    }
  }
    
    if (this.isEditing) {
        const publicationDate = new Date();
        const updatedEntry: NewEntry = {
            id: this.currentEntry.id,
            title: this.currentEntry.title,
            content: this.currentEntry.content,
            category: this.currentEntry.category,
            user: localStorage.getItem('userId') || '', 
        };
        
        this.entryService.updateEntry(updatedEntry).subscribe({
            next: () => {
                this.loadEntries();
                this.isFormVisible = false;
            },
            error: (error) => {
                console.error('Error al actualizar la entrada', error);
            }
        });
    } else {
        this.onCreateEntry();
    }
}




  onCreateEntry() {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.currentEntry.user = userId; 
    }
    this.entryService.createEntry(this.currentEntry).subscribe({
      next: (response) => {
        this.loadEntries();
        this.isFormVisible = false;
      },
      error: (error) => {
        console.error('Error al crear la entrada', error);
      }
    });
  }

  onFilterChange() {
    this.loadEntries();
}


  deleteEntry(entryId: string) {
    if (confirm('Are you sure you want to delete this entry?')) {
      this.entryService.deleteEntry(entryId).subscribe({
        next: () => {
          this.loadEntries();
        },
        error: (err) => {
          console.error('Error deleting entry', err);
        }
      });
    }
  }
  createCategory() {
    if (this.newCategoryName.trim() === '') {
      return;
    }
  
    this.categoryService.createCategory(this.newCategoryName)
      .subscribe(newCategory => {
        this.categories.push(newCategory); 
        this.newCategoryName = ''; 
        this.isCategoryFormVisible = false; 
      });
      this.loadEntries();
  }
  
  toggleCategoryForm() {
    this.isCategoryFormVisible = !this.isCategoryFormVisible;
  }

  changePage(page: number) {
    if (page < 1 || page * this.pageSize > this.totalEntries) {
        return; 
    }
    this.currentPage = page;
    this.loadEntries(); 
  }

}
