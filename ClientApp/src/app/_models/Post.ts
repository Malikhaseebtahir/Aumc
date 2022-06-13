import { keyValuePairs } from "./PostCategory";

export interface Post {
    id: number;
    title: string;
    datePosted: string;
    postCategory: keyValuePairs;
}
