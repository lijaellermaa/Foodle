export enum SortDirection {
  None,
  Ascending,
  Descending,
}

export type FilterQuery = {
  limit?: number;
  offset?: number;
  sortBy?: SortDirection;
  searchQuery?: string;
}
