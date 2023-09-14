import { MediaItemType } from "../enums/MediaItemType";
import MediaItemId from "../valueObjects/MediaItemId";

export default interface MediaItem {
    mediaItemId: MediaItemId;
    type: MediaItemType;
    url: string;
}