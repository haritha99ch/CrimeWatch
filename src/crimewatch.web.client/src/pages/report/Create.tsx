import {ChangeEvent, FormEvent, useState} from "react";
import {CreateReportDto} from "../../models/Report";
import {CreateReport} from "../../services/ReportService";
import {UseAuthenticationContextProvider} from "../../providers/AuthenticationContextProvider";
import {useNavigate} from "react-router-dom";
import Witness from "../../models/Witness";

const Create = () => {
    const {currentUser, currentUserLoading} =
        UseAuthenticationContextProvider();
    const navigate = useNavigate();
    if (!currentUserLoading && !currentUser) {
        navigate("/Account/SignIn");
    }

    const [form, setForm] = useState({
        title: "",
        description: "",
        addressNo: "",
        addressStreet1: "",
        addressStreet2: "",
        addressCity: "",
        addressProvince: "",
        mediaItem: new File([], ""),
    });

    const handleInput = (
        event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
        if ((event.target as HTMLInputElement).files) {
            const file = (event.target as HTMLInputElement).files![0];
            setForm(() => ({
                ...form,
                [event.target.name]: file,
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
        const report: CreateReportDto = {
            witnessId: (currentUser as Witness).id,
            title: form.title,
            description: form.description,
            location: {
                no: form.addressNo,
                street1: form.addressStreet1,
                street2: form.addressStreet2,
                city: form.addressCity,
                province: form.addressProvince,
            },
            mediaItem: form.mediaItem ?? undefined,
        };
        const newReport = await CreateReport(report);
        if (!newReport) return; // TODO: Something went wrong
        navigate(`/Report/Details/${newReport.id.value}`);
    };

    const content: JSX.Element = (
        <>
            <div className="h-screen">
                <div className="h-[80px]"></div>
                <div className="mx-auto w-full p-0 md:p-4 md:mx-auto md:w-1/2">
                    <div className="md:container md:mx-auto container mx-auto dark:dark-mode-text-primary">
                        <div className="px-4 sm:px-0">
                            <h3 className="text-base font-semibold leading-7 text-gray-900 dark:dark-mode-text-primary">
                                Create a Report
                            </h3>
                            <p className="mt-1 max-w-2xl text-sm leading-6 text-gray-500 dark:dark-mode-text-tertiary">
                                Report details.
                            </p>
                        </div>
                        <form className="report-form" onSubmit={handleSubmit}>
                            <div className="mt-6 border-t border-gray-100">
                                <dl className="divide-y divide-gray-100">
                                    <div className="report-description-list-group">
                                        <dt className="description">
                                            <label htmlFor="title" className="report-form-label">
                                                Report Title
                                            </label>
                                        </dt>
                                        <dd>
                                            <input
                                                className="form-input"
                                                type="text"
                                                name="title"
                                                id="title"
                                                placeholder="Report Tile"
                                                required
                                                value={form.title}
                                                onChange={handleInput}
                                            />
                                        </dd>
                                    </div>
                                    <div className="report-description-list-group">
                                        <dt className="description">
                                            <label
                                                htmlFor="description"
                                                className="report-form-label">
                                                Report Description
                                            </label>
                                        </dt>
                                        <dd>
                      <textarea
                          className="form-input"
                          rows={4}
                          name="description"
                          id="description"
                          placeholder="Report Description"
                          required
                          value={form.description}
                          onChange={handleInput}
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
                                                        htmlFor="address-no"
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
                                                            required
                                                            value={form.addressNo}
                                                            onChange={handleInput}
                                                        />
                                                    </div>
                                                </div>
                                                <div className="basis-1/3">
                                                    <label
                                                        htmlFor="address-street1"
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
                                                            value={form.addressStreet1}
                                                            onChange={handleInput}
                                                        />
                                                    </div>
                                                </div>
                                                <div className="basis-1/3">
                                                    <label
                                                        htmlFor="address-street2"
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
                                                            required
                                                            value={form.addressStreet2}
                                                            onChange={handleInput}
                                                        />
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="flex flex-row gap-2">
                                                <div className="basis-1/3">
                                                    <label
                                                        htmlFor="address-city"
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
                                                            value={form.addressCity}
                                                            onChange={handleInput}
                                                        />
                                                    </div>
                                                </div>
                                                <div className="basis-1/3">
                                                    <label
                                                        htmlFor="address-city"
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
                                                            value={form.addressProvince}
                                                            onChange={handleInput}
                                                        />
                                                    </div>
                                                </div>
                                                <div className="basis-1/3"></div>
                                            </div>
                                        </dt>
                                    </div>
                                    <div className="report-description-list-group">
                                        <dt className="description">
                                            <label htmlFor="mediaItem" className="report-form-label">
                                                Image
                                            </label>
                                            <p className="mt-1 max-w-2xl text-sm leading-6 text-gray-500 dark:dark-mode-text-tertiary">
                                                Include one image.
                                            </p>
                                        </dt>
                                        <dt className="">
                                            <input
                                                type="file"
                                                name="mediaItem"
                                                id="mediaItem"
                                                required
                                                onChange={handleInput}
                                            />
                                        </dt>
                                    </div>
                                </dl>
                            </div>
                            <div className="flex flex-row gap-3">
                                <button className="px-2 py-2" type="submit">
                                    Create Report
                                </button>
                                <button className="px-2 py-2 text-red-500" type="reset">
                                    Cancel
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </>
    );
    return content;
};

export default Create;
