export interface Entry {
    id: string; 
    title: string;
    content: string;
    publicationDate: Date;
    userName: string; 
    categoryName: string; 
  }

  export interface NewEntry {
    id: string;
    title: string;
    content: string;
    category: string;
    user: string;
  }