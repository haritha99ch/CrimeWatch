import Evidence from "../models/Evidence";

const EvidenceListItem = ({ evidence }: { evidence: Evidence }) => {
  console.log(evidence);

  return (
    <>
      <div className="border-gray-400 border-2 rounded-2xl p-4 mb-2 dark:dark-mode-text-primary">
        <p key={evidence.id.value}>{evidence.title}</p>
        <p>
          {evidence.location.no}, {evidence?.location.street1},{" "}
          {evidence.location.street2}, {evidence?.location.city},{" "}
          {evidence.location.province}
        </p>
        <p className="mt-1 max-w-2xl text-sm leading-6 text-gray-500 dark:dark-mode-text-tertiary">
          {new Date(evidence.dateTime).toLocaleString()}
        </p>
        <p>
          {evidence.witness?.user?.firstName} {evidence.witness?.user?.lastName}
        </p>
        <p>{evidence.description}</p>
        <p>{evidence.status}</p>
        <p>
          Moderated By: {evidence.moderator?.user?.firstName}{" "}
          {evidence.moderator?.user?.lastName}
        </p>
        <div id="evidence-images">
          {evidence.mediaItems.map(mediaItem => (
            <img className="w-full" src={mediaItem.url} alt={evidence.title} />
          ))}
        </div>
      </div>
    </>
  );
};

export default EvidenceListItem;
