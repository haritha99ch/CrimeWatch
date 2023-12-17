import {createPortal} from "react-dom";
import AddEvidenceModalType from "../types/AddEvidenceModalType";
import {ChangeEvent, FormEvent, useState} from "react";
import {CreateEvidenceDto} from "../models/Evidence";
import {CreateEvidence} from "../services/EvidenceService";

const AddEvidenceModal = ({modal}: { modal: AddEvidenceModalType }) => {
    const [form, setForm] = useState({
        title: "",
        description: "",
        addressNo: "",
        addressStreet1: "",
        addressStreet2: "",
        addressCity: "",
        addressProvince: "",
        mediaItems: [],
    });

    const handleInput = (
        event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
        if ((event.target as HTMLInputElement).files) {
            setForm(() => ({
                ...form,
                [event.target.name]: (event.target as HTMLInputElement).files,
            }));
            return;
        }
        setForm(() => ({
            ...form,
            [event.target.name]: event.target.value,
        }));
    };

    const handleSubmit = async (e: FormEvent<HTMLFormElement>): Promise<void> => {
        e.preventDefault();
        const evidence: CreateEvidenceDto = {
            reportId: modal.reportId!,
            witnessId: modal.witnessId!,
            caption: form.title,
            description: form.description,
            location: {
                no: form.addressNo,
                street1: form.addressStreet1,
                street2: form.addressStreet2,
                city: form.addressCity,
                province: form.addressProvince,
            },
            mediaItems: form.mediaItems,
        };
        const newEvidence = await CreateEvidence(evidence);
        if (!newEvidence) return; // TODO: Something went wrong.
        modal.onSubmit(newEvidence);
        modal.closeModal();
    };

    const formContent: JSX.Element = (
        <>
            <div className="h-screen overflow-auto">
                <div className="h-[80px]"></div>
                <div className="mx-auto w-full p-0 md:p-4 md:mx-auto md:w-1/2">
                    <div
                        className="md:container md:mx-auto container mx-auto dark:dark-mode-text-primary bg-white dark:dark-mode-bg-primary p-4 rounded-2xl">
                        <div className="px-4 sm:px-0">
                            <h3 className="text-base font-semibold leading-7 text-gray-900 dark:dark-mode-text-primary">
                                Add an Evidence
                            </h3>
                            <p className="mt-1 max-w-2xl text-sm leading-6 text-gray-500 dark:dark-mode-text-tertiary">
                                Evidence details.
                            </p>
                        </div>
                        <form className="report-form" onSubmit={handleSubmit}>
                            <div className="mt-6 border-t border-gray-100">
                                <dl className="divide-y divide-gray-100">
                                    <div className="report-description-list-group">
                                        <dt className="description">
                                            <label htmlFor="title" className="report-form-label">
                                                Evidence Title
                                            </label>
                                        </dt>
                                        <dd>
                                            <input
                                                className="form-input"
                                                type="text"
                                                name="title"
                                                id="title"
                                                placeholder="Evidence Tile"
                                                required
                                                onChange={handleInput}
                                                value={form.title}
                                            />
                                        </dd>
                                    </div>
                                    <div className="report-description-list-group">
                                        <dt className="description">
                                            <label
                                                htmlFor="description"
                                                className="report-form-label">
                                                Evidence Description
                                            </label>
                                        </dt>
                                        <dd>
                      <textarea
                          className="form-input"
                          rows={4}
                          name="description"
                          id="description"
                          placeholder="Evidence Description"
                          required
                          onChange={handleInput}
                          value={form.description}
                      />
                                        </dd>
                                    </div>
                                    <div className="report-description-list-group">
                                        <dt className="description">
                                            <label className="report-form-label">Address</label>
                                        </dt>
                                        <dt className="">
                                            <div className="flex flex-row gap-2">
                                                <div className="basis-1/3">
                                                    <label
                                                        htmlFor="addressNo"
                                                        className="report-form-label">
                                                        No
                                                    </label>
                                                    <div className="w-full">
                                                        <input
                                                            className="form-input"
                                                            type="text"
                                                            name="addressNo"
                                                            id="addressNo"
                                                            placeholder="No"
                                                            autoComplete="address-level1"
                                                            onChange={handleInput}
                                                            value={form.addressNo}
                                                        />
                                                    </div>
                                                </div>
                                                <div className="basis-1/3">
                                                    <label
                                                        htmlFor="addressStreet1"
                                                        className="report-form-label">
                                                        Street 1
                                                    </label>
                                                    <div className="w-full">
                                                        <input
                                                            className="form-input"
                                                            type="text"
                                                            name="addressStreet1"
                                                            id="addressStreet1"
                                                            placeholder="Street 1"
                                                            autoComplete="address-level2"
                                                            required
                                                            onChange={handleInput}
                                                            value={form.addressStreet1}
                                                        />
                                                    </div>
                                                </div>
                                                <div className="basis-1/3">
                                                    <label
                                                        htmlFor="addressStreet2"
                                                        className="report-form-label">
                                                        Street 2
                                                    </label>
                                                    <div className="w-full">
                                                        <input
                                                            className="form-input"
                                                            type="text"
                                                            name="addressStreet2"
                                                            id="addressStreet2"
                                                            placeholder="Street 2"
                                                            autoComplete="address-level3"
                                                            onChange={handleInput}
                                                            value={form.addressStreet2}
                                                        />
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="flex flex-row gap-2">
                                                <div className="basis-1/3">
                                                    <label
                                                        htmlFor="addressCity"
                                                        className="report-form-label">
                                                        City
                                                    </label>
                                                    <div className="w-full">
                                                        <input
                                                            className="form-input"
                                                            type="text"
                                                            name="addressCity"
                                                            id="addressCity"
                                                            placeholder="City"
                                                            autoComplete="address-level2"
                                                            required
                                                            onChange={handleInput}
                                                            value={form.addressCity}
                                                        />
                                                    </div>
                                                </div>
                                                <div className="basis-1/3">
                                                    <label
                                                        htmlFor="addressProvince"
                                                        className="report-form-label">
                                                        Province
                                                    </label>
                                                    <div className="w-full">
                                                        <input
                                                            className="form-input"
                                                            type="text"
                                                            name="addressProvince"
                                                            id="addressProvince"
                                                            placeholder="Province"
                                                            required
                                                            onChange={handleInput}
                                                            value={form.addressProvince}
                                                        />
                                                    </div>
                                                </div>
                                                <div className="basis-1/3"></div>
                                            </div>
                                        </dt>
                                    </div>
                                    <div className="report-description-list-group">
                                        <dt className="description">
                                            <label htmlFor="mediaItems" className="report-form-label">
                                                Images
                                            </label>
                                            <p className="mt-1 max-w-2xl text-sm leading-6 text-gray-500 dark:dark-mode-text-tertiary">
                                                Include images.
                                            </p>
                                        </dt>
                                        <dt className="">
                                            <input
                                                className="form-input"
                                                type="file"
                                                name="mediaItems"
                                                id="mediaItems"
                                                placeholder="Media Items"
                                                required
                                                multiple
                                                accept="image/png, image/jpeg"
                                                onChange={handleInput}
                                            />
                                        </dt>
                                    </div>
                                </dl>
                            </div>
                            <div className="flex flex-row gap-3">
                                <button className="px-2 py-2" type="submit">
                                    Create Evidence
                                </button>
                                <button
                                    className="px-2 py-2 text-red-500"
                                    type="reset"
                                    onClick={modal.closeModal}>
                                    Cancel
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </>
    );

    const content = createPortal(
        <>
            <div className="fixed top-0 left-0 right-0 bottom-0 bg-black/50">
                {formContent}
            </div>
        </>,
        document.getElementById("portal")!
    );

    return <>{modal.isOpen && <>{content}</>}</>;
};

export default AddEvidenceModal;
