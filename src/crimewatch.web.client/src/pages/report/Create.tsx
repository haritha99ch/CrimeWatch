const Create = () => {
    const content :JSX.Element = <>
    <div className="h-[80px]"></div>
    <div className="mx-auto w-full p-0 md:p-4 md:mx-auto md:w-1/2 ">
        <div className="md:container md:mx-auto container mx-auto">
            <div className="px-4 sm:px-0">
                <h3 className="text-base font-semibold leading-7 text-gray-900">Create a Report</h3>
                <p className="mt-1 max-w-2xl text-sm leading-6 text-gray-500">Report details.</p>
            </div>
            <form className="report-form">
            <div className="mt-6 border-t border-gray-100">
                <dl className="divide-y divide-gray-100">
                    <div className="report-description-list-group">
                        <dt className="description">Report Title</dt>
                        <dd>
                            <input className="form-input"
                                type="text" name="title" id="title" placeholder="Report Tile"/>
                        </dd>
                    </div>
                    <div className="report-description-list-group">
                        <dt className="description">Report Description</dt>
                        <dd>
                            <textarea className="form-input"
                                rows={4} name="description" id="description" placeholder="Report Description"/>
                        </dd>
                    </div>
                    <div className="report-description-list-group">
                        <dt className="description">Address</dt>
                        <dt className="">
                            <div className="flex flex-row gap-11">
                                <div className="basis-1/3">
                                    <label htmlFor="address-no" className="report-form-label">No</label>
                                    <div className="w-full">
                                        <input className="form-input"
                                            type="text" name="address-no" id="address-no" autoComplete="address-level1" />
                                    </div>
                                </div>
                                <div className="basis-1/3">
                                    <label htmlFor="address-street1" className="report-form-label">Street 1</label>
                                    <div className="w-full">
                                        <input className="form-input"
                                            type="text" name="address-street1" id="address-street1" autoComplete="address-level2"/>
                                    </div>
                                </div>
                                <div className="basis-1/3">
                                    <label htmlFor="address-street2" className="report-form-label">Street 2</label>
                                    <div className="w-full">
                                        <input className="form-input"
                                            type="text" name="address-street2" id="address-street2" autoComplete="address-level3"/>
                                    </div>
                                </div>
                            </div>
                            <div className="flex flex-row gap-11">
                                <div className="basis-1/3">
                                    <label htmlFor="address-city" className="report-form-label">City</label>
                                    <div className="w-full">
                                        <input className="form-input"
                                            type="text" name="address-city" id="address-city" autoComplete="address-level4"/>
                                    </div>
                                </div>
                                <div className="basis-1/3">
                                    <label htmlFor="address-city" className="report-form-label">Province</label>
                                    <div className="w-full">
                                        <input className="form-input"
                                            type="text" name="address-province" id="address-city"/>
                                    </div>
                                </div>
                            </div>
                        </dt>
                    </div>
                    <div className="report-description-list-group">
                        <dt className="description">
                            <label htmlFor="mediaItem" className="report-form-label">Image</label>
                            <p className="mt-1 max-w-2xl text-sm leading-6 text-gray-500">Include one image.</p>
                        </dt>
                        <dt className="">
                            <input type="file" name="mediaItem" id="mediaItem" />
                        </dt>
                    </div>
                </dl>
            </div>
            </form>
        </div>
    </div>
    </> 
  return content;
}

export default Create