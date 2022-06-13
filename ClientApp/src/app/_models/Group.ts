import { Post } from './Post';

export interface Group {
    id: number;
    className: string;
    section: string;
    subject: string;
    description: string;
    room: number;
    groupType: {
        id: number;
        name: string;
    };
    lastUpdated: string;
    dateCreated: string;
    user: {
        id: number;
        knownAs: string;
        lastActive: string;
        teacherOrStudent: string;
    };

    posts: Post[];
}
