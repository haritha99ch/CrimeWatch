import { FormEvent, useState } from "react";
import { CreateReportDto } from "../../models/Report";
import { GetCurrentUser } from "../../services/AccountService";
import Witness from "../../models/Witness";
import { CreateReport } from "../../services/ReportService";

// TODO: current user context
const currentUser = await GetCurrentUser();

const Create = () => {

    // If the user is not logged in, redirect to the login page.
    if (!currentUser) {
        // window.location.href = "/Account/SignIn";
    }

    const [title, setTitle] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [addressNo, setAddressNo] = useState<string>("");
    const [addressStreet1, setAddressStreet1] = useState<string>("");
    const [addressStreet2, setAddressStreet2] = useState<string>("");
    const [addressCity, setAddressCity] = useState<string>("");
    const [addressProvince, setAddressProvince] = useState<string>("");
    const [mediaItem, setMediaItem] = useState<File | null>(null);

    const handleSubmit = async(e: FormEvent<HTMLFormElement>): Promise<void> => {
        e.preventDefault();

        if (currentUser.account!.isModerator) return;

        const report: CreateReportDto = {
            witnessId: (currentUser as Witness).witnessId ?? undefined,
            title: title,
            description: description,
            location: {
                no: addressNo,
                street1: addressStreet1,
                street2: addressStreet2,
                city: addressCity,
                province: addressProvince
            },
            mediaItem: mediaItem ?? undefined
        }
        await CreateReport(report);
    }


    const content :JSX.Element = <>
    <div className="h-screen">
        <div className="h-[80px]"></div>
        <div className="mx-auto w-full p-0 md:p-4 md:mx-auto md:w-1/2">
            <div className="md:container md:mx-auto container mx-auto dark:dark-mode-text-primary">
                <div className="px-4 sm:px-0">
                    <h3 className="text-base font-semibold leading-7 text-gray-900 dark:dark-mode-text-primary">Create a Report</h3>
                    <p className="mt-1 max-w-2xl text-sm leading-6 text-gray-500 dark:dark-mode-text-tertiary">Report details.</p>
                </div>
                <form className="report-form" onSubmit={handleSubmit}>
                    <div className="mt-6 border-t border-gray-100">
                        <dl className="divide-y divide-gray-100">
                            <div className="report-description-list-group">
                                <dt className="description">
                                    <label htmlFor="title" className="report-form-label">Report Title</label>
                                </dt>
                                <dd>
                                    <input className="form-input"
                                        type="text" name="title" id="title" placeholder="Report Tile"
                                        required value={title} onChange={e=>setTitle(e.target.value)}/>
                                </dd>
                            </div>
                            <div className="report-description-list-group">
                            <dt className="description">
                                    <label htmlFor="description" className="report-form-label">Report Description</label>
                                </dt>
                                <dd>
                                    <textarea className="form-input"
                                        rows={4} name="description" id="description" placeholder="Report Description"
                                        required value={description} onChange={e=>setDescription(e.target.value)}/>
                                </dd>
                            </div>
                            <div className="report-description-list-group">
                                <dt className="description">
                                    <label  className="report-form-label">Address</label>
                                </dt>
                                <dt className="">
                                    <div className="flex flex-row gap-2">
                                        <div className="basis-1/3">
                                            <label htmlFor="address-no" className="report-form-label">No</label>
                                            <div className="w-full">
                                                <input className="form-input"
                                                    type="text" name="address-no" id="address-no" placeholder="No" autoComplete="address-level1"
                                                    required value={addressNo} onChange={e=>setAddressNo(e.target.value)} />
                                            </div>
                                        </div>
                                        <div className="basis-1/3">
                                            <label htmlFor="address-street1" className="report-form-label">Street 1</label>
                                            <div className="w-full">
                                                <input className="form-input"
                                                    type="text" name="address-street1" id="address-street1" placeholder="Street 1" autoComplete="address-level2"
                                                    required value={addressStreet1} onChange={e=>setAddressStreet1(e.target.value)}/>
                                            </div>
                                        </div>
                                        <div className="basis-1/3">
                                            <label htmlFor="address-street2" className="report-form-label">Street 2</label>
                                            <div className="w-full">
                                                <input className="form-input"
                                                    type="text" name="address-street2" id="address-street2" placeholder="Street 2" autoComplete="address-level3"
                                                    required value={addressStreet2} onChange={e=>setAddressStreet2(e.target.value)}/>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="flex flex-row gap-2">
                                        <div className="basis-1/3">
                                            <label htmlFor="address-city" className="report-form-label">City</label>
                                            <div className="w-full">
                                                <input className="form-input"
                                                    type="text" name="address-city" id="address-city" placeholder="City" autoComplete="address-level4"
                                                    required value={addressCity} onChange={e=>setAddressCity(e.target.value)}/>
                                            </div>
                                        </div>
                                        <div className="basis-1/3">
                                            <label htmlFor="address-city" className="report-form-label">Province</label>
                                            <div className="w-full">
                                                <input className="form-input"
                                                    type="text" name="address-province" id="address-city" placeholder="Province"
                                                    required value={addressProvince} onChange={e=>setAddressProvince(e.target.value)}/>
                                            </div>
                                        </div>
                                        <div className="basis-1/3"></div>
                                    </div>
                                </dt>
                            </div>
                            <div className="report-description-list-group">
                                <dt className="description">
                                    <label htmlFor="mediaItem" className="report-form-label">Image</label>
                                    <p className="mt-1 max-w-2xl text-sm leading-6 text-gray-500 dark:dark-mode-text-tertiary">Include one image.</p>
                                </dt>
                                <dt className="">
                                    <input type="file" name="mediaItem" id="mediaItem" 
                                        required onChange={e=>setMediaItem(e.target.files![0])}/>
                                </dt>
                            </div>
                        </dl>
                    </div>
                    <div className="flex flex-row gap-3">
                        <button className="px-2 py-2" type="submit">Create Report</button>
                        <button className="px-2 py-2 text-red-500" type="reset">Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
    </> 
  return content;
}

export default Create;

